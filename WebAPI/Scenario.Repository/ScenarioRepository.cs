using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scenario.Entities;
using System.ComponentModel;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;


namespace Scenario.Repository
{
    public class ScenarioRepository : IScenarioRepository
    {
        protected testEntityFrameworkEntities context;

        public ScenarioRepository()
        {
            context = new testEntityFrameworkEntities();
        }
        public ScenarioRepository(string ConnectionString)
        {
            context = new testEntityFrameworkEntities(ConnectionString);
        }

        public IList<IResult> GetResults(Configuration Scenario, params int[] Trials)
        {

            IList<IResult> results = new List<IResult>();

            foreach (int Trial in Trials)
            {
                var resMc1 = context.CreateNavigationSourceQuery<CURVES_MC_1>(Scenario, "CURVES_MC_1");
                resMc1.Where<CURVES_MC_1>(c => c.Trial.Equals(Trial)).ToList<IResult>().ForEach(x => results.Add(x));
                var resMc2 = context.CreateNavigationSourceQuery<CURVES_MC_2>(Scenario, "CURVES_MC_2");
                resMc2.Where<CURVES_MC_2>(c => c.Trial.Equals(Trial)).ToList<IResult>().ForEach(x => results.Add(x));
                var resRealc = context.CreateNavigationSourceQuery<REALC>(Scenario, "REALCs");
                resRealc.Where<REALC>(c => c.Trial.Equals(Trial)).ToList<IResult>().ForEach(x => results.Add(x));
                var resSummary = context.CreateNavigationSourceQuery<SUMMARY>(Scenario, "SUMMARies");
                resSummary.Where<SUMMARY>(c => c.Trial.Equals(Trial)).ToList<IResult>().ForEach(x => results.Add(x));
                var resExtra = context.CreateNavigationSourceQuery<EXTRA>(Scenario, "EXTRAs");
                resExtra.Where<EXTRA>(c => c.Trial.Equals(Trial)).ToList<IResult>().ForEach(x => results.Add(x));
                var resCtm = context.CreateNavigationSourceQuery<TRANSITION_MATRIX>(Scenario, "TRANSITION_MATRIX");
                resCtm.Where<TRANSITION_MATRIX>(c => c.Trial.Equals(Trial)).ToList<IResult>().ForEach(x => results.Add(x));
            }
            return results;
        }

        public void ClearResultsFor(Configuration Scenario, int UpToTrial = 0)
        {
            string[] tables = new string[] { "CURVES_MC_1", "CURVES_MC_2", "REALC", "SUMMARY", "EXTRA", "TRANSITION_MATRIX" };
            string filter = "";
            if (UpToTrial > 0)
            {
                filter = "and Trial <= " + UpToTrial;
            }
            foreach (string table in tables)
            {
                int rows = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreCommand("delete from " + table + " where ScenarioId = {0} " + filter, Scenario.ID);
            }
        }

        private void SaveScenario(Configuration Scenario)
        {
             if (Scenario.ID == 0)
            {
                context.Configurations.Add(Scenario);
            }
            System.Data.Entity.Core.Objects.ObjectStateEntry entry;
            if (((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.TryGetObjectStateEntry(((IObjectContextAdapter)context).ObjectContext.CreateEntityKey("Configurations", Scenario), out entry))
            {
                ((IObjectContextAdapter)context).ObjectContext.ApplyCurrentValues("Configurations", Scenario);
            }
            context.SaveChanges();
            if (Scenario.Results != null)
            {
                foreach (Configuration conf in Scenario.SelfAndChildren)
                    context.AddResultsToDatabase(conf);
            }
        }

        public void Save(Configuration Scenario)
        {
            try
            {
                SaveScenario(Scenario);
            }
            catch (Exception ex)
            {
                throw new ScenarioEntityException("Cannot save configuration " + Scenario.Identifyer, ex);
            }
        }

        public void SaveCreditFor(Configuration Scenario)
        {
            if (Scenario.ID != 0)
            {
                try
                {
                    foreach (Credit credit in Scenario.Credits)
                    {
                        ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreCommand(
                            "update Credit set Pi = {1}, Spread={2}, Price={3} where ID = {0}",
                            credit.ID, credit.Pi, credit.Spread, credit.Price);
                    }
                }
                catch (Exception e)
                {
                    throw new ScenarioEntityException("Cannot save credit fof configuration " + Scenario.Identifyer, e);
                }
            }

        }

        public IList<ScenarioSummary> GetAll(bool OnlyMainEconomy = true)
        {
            if (OnlyMainEconomy)
                return context.ScenarioSummaries.Where(conf => conf.ParentId == null).ToList<ScenarioSummary>();
            else
                return context.ScenarioSummaries.ToList<ScenarioSummary>();
        }

        public IList<Configuration> GetScenariosOfType(ScenarioType Type)
        {
            return context.Configurations.Where(conf => conf.ScenarioType.ID == Type.ID).ToList<Configuration>();
        }

        public void Delete(Configuration config)
        {
            try
            {
                config.Children.ToList().ForEach(cfg => Delete(cfg));
                ((IObjectContextAdapter)context).ObjectContext.DeleteObject(config);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ScenarioEntityException("Cannot delete configuration " + config.Identifyer, e);
            }
        }

        public void DeleteCredits(Configuration Scenario)
        {
            try
            {
                //Scenario.Credits.ToList().ForEach(c => ((IObjectContextAdapter)context).ObjectContext.DeleteObject(c));
                Scenario.Credits.ToList().ForEach(c => context.Credits.Remove(c));
                
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ScenarioEntityException("Cannot delete credits for scenario " + Scenario.Identifyer, e);
            }

        }

        public Configuration Load(int configId)
        {
            string connection = ((IObjectContextAdapter)context).ObjectContext.Connection.ConnectionString;
            context.Dispose();
            context = new testEntityFrameworkEntities(connection);
            context.Configuration.LazyLoadingEnabled = true;
            
            return Get(configId);
        }

        public Configuration GetBaseFor(Configuration Scenario, bool SameDate = true)
        {
            Configuration baseScenario = null;
            string baseType = Scenario.ScenarioType.ModelType;
            var bases = GetBases(Scenario, SameDate);
            if (bases != null)
                bases = bases.Where(b => b.IsReleased
                    && b.ScenarioType.ModelType.Equals(baseType)
                    && b.ScenarioType.Country == Scenario.ScenarioType.Country
                    && b.ScenarioType.IsBase
                    && b.ModelParameter.Models.TypeDescription.Equals(Scenario.ModelParameter.Models.TypeDescription)).ToList();
            if (bases.Count == 0)
            {
                bases = GetBases(Scenario, SameDate);
                bases = bases.Where(b => b.IsReleased
                    && (b.ScenarioType.ModelType.Equals(Scenario.ScenarioType.ReferenceBaseType))
                    && b.ScenarioType.Country.Equals(Scenario.ScenarioType.Country)
                    && b.ScenarioType.IsBase
                    && b.ModelParameter.Models.TypeDescription.Equals(Scenario.ModelParameter.Models.TypeDescription)).ToList();
            }

            if (bases.Count > 0)
            {
                DateTime maxDate = bases.Max(s => s.ScenarioType.ScenarioDate);
                baseScenario = bases.Where(b => b.ScenarioType.ScenarioDate.Equals(maxDate)).First();

            }
            return baseScenario;
        }

        private IList<Configuration> GetBases(Configuration Scenario, bool SameDate)
        {
            DateTime inf = Scenario.ScenarioType.ScenarioDate.AddYears(-1);
            var bases = context.Configurations.Where(
            a => a.ScenarioType.ScenarioDate <= Scenario.ScenarioType.ScenarioDate
                && a.ScenarioType.ScenarioDate >= inf
              && a.ScenarioType.Economy.Equals(Scenario.ScenarioType.Economy)
              && a.ScenarioType.Country.Equals(Scenario.ScenarioType.Country)
              && a.Economy.Equals(Scenario.Economy) && a.ID != Scenario.ID
            ).ToList();

            if (SameDate)
                bases = bases.Where(
                a => a.ScenarioType.ScenarioDate.Equals(Scenario.ScenarioType.ScenarioDate)).ToList();
            return bases;
        }

        public Configuration Get(int configId)
        {
            try
            {
                Configuration c = context.Configurations.First(conf => conf.ID == configId);
                context.Configurations.Include("Reals").Single(s => s.ID == configId);
                if(c.Credits.Count > 0)
                    context.Credits.Include("DefaultCredit").Single(cr => cr.ConfigId == configId); 
                return c;
            }
            catch (Exception e)
            {
                throw new ScenarioEntityException("Cannot get configuration id " + configId, e);
            }
        }

        public Configuration Get(ScenarioType type)
        {
            try
            {
                string[] economies = type.Economy.Split(ScenarioType.EconomySeparator);
                string economy = economies[0];

                Configuration ret = context.Configurations.Where(
                        conf => (type.ID == conf.TypeId)
                        ).First<Configuration>();
                for (int i = 1; i < economies.Length; i++)
                {
                    if (ret.Children.ElementAt(i - 1).Economy != economies[i])
                        throw new Exception("Children type not equal");
                }

                return ret;
            }
            catch (Exception e)
            {
                throw new ScenarioEntityException("Cannot get configuration type", e);
            }
        }

        public IList<Equity> GetEquitiesFor(Configuration Scenario)
        {
            try
            {
                string type = Scenario.ScenarioType.TypeForEquity;
                return RepoHelper.GetCurvesFor<Equity>(context.Equities, type, Scenario);
            }
            catch { throw new ScenarioEntityException(ErroMessage("Equities", Scenario)); }
        }


        public SwaptionCurve GetMarketSwaptionsFor(Configuration Scenario)
        {
            try
            {
                int? simulated = Scenario.Simulated;
                Scenario.Simulated = 0;
                var marketSwaptions = GetSwaptionsFor(Scenario);
                Scenario.Simulated = simulated;

                return marketSwaptions;
            }
            catch { throw new ScenarioEntityException(ErroMessage("Swaptions", Scenario)); }
        }

        public SwaptionCurve GetSwaptionsFor(Configuration Scenario)
        {
            try
            {
                string type = Scenario.ScenarioType.TypeForSwaption;
                return GetSwaptionForType(Scenario, type);
            }
            catch
            {
                try
                {
                    return GetSwaptionForType(Scenario, Scenario.ScenarioType.BaseTypeForSwaption);
                }
                catch { throw new ScenarioEntityException(ErroMessage("Swaptions", Scenario)); }
            }

        }

        private SwaptionCurve GetSwaptionForType(Configuration Scenario, string Type)
        {
            if (Scenario.LoadSimulated)
            {
                Type += "_SIMULATED";
            }

            var curves = RepoHelper.GetCurvesFor<SwaptionCurve>(context.SwaptionCurves, Type, Scenario);
            return curves.Where(cc => cc.GID == curves.Max(c => c.GID)).First();
        }

        private TestWeight GetTestWeightsFor(Configuration Scenario, string Type, string Economy) {
            try
            {
                var tests = context.TestWeights.Where(t => t.Type.Equals(Type)
                    && t.Date <= Scenario.ScenarioType.ScenarioDate
                       && t.Economy.Equals(Economy));
                var maxDate = tests.Max(t => t.Date);
                int gid = tests.Where(t => t.Date.Equals(maxDate)).First().GID;
                return context.TestWeights.Where(cc => cc.GID == gid).First();
            }catch(Exception){ return null; }
       
        }

        public TestWeight GetTestWeightsFor(Configuration Scenario)
        {
            string type = Scenario.ScenarioType.TypeForTestWeights;
            string economy = RepoHelper.GetScenarioEconomy(Scenario);

            TestWeight t;
            t = GetTestWeightsFor(Scenario, type, economy);
            if(t == null){
                type = Scenario.ScenarioType.TypeForTestWeights;
                economy = Scenario.Economy;
                t = GetTestWeightsFor(Scenario, type, economy);
            }
            if(t == null){
                type = Scenario.ScenarioType.BaseTypeForTestWeights;
                economy = RepoHelper.GetScenarioEconomy(Scenario);
                t = GetTestWeightsFor(Scenario, type, economy);
            }
            if (t == null)
            {
                type = Scenario.ScenarioType.BaseTypeForTestWeights;
                economy = Scenario.Economy;
                t = GetTestWeightsFor(Scenario, type, economy);
            }

            return t;
        }

        public InflationCurve GetInflationsFor(Configuration Scenario)
        {
            return GetInflationsFor(Scenario, "ALL");
        }

        public InflationCurve GetCertaintyEquivalentInflationsFor(Configuration Scenario)
        {
            return GetInflationsFor(Scenario, "SES");
        }

        private InflationCurve GetInflationsFor(Configuration Scenario, string InflationType)
        {

            string type = Scenario.ScenarioType.TypeForInflation + RepoHelper.GetTypeMonthlyAnnual(InflationType, Scenario.ModelParameter.Models.IsMonthly);
            try
            {
                var curves = RepoHelper.GetCurvesFor<InflationCurve>(context.InflationCurves, type, Scenario);
                return curves.Where(cc => cc.GID == curves.Max(c => c.GID)).First();

            }
            catch
            {
                type = RepoHelper.GetTypeMonthlyAnnual(InflationType, Scenario.ModelParameter.Models.IsMonthly);
                try
                {
                    var curves = RepoHelper.GetCurvesFor<InflationCurve>(context.InflationCurves, type, Scenario);
                    return curves.Where(cc => cc.GID == curves.Max(c => c.GID)).First();
                }
                catch { throw new ScenarioEntityException(ErroMessage("Inflations", Scenario)); }
            }
        }



        public IList<Property> GetPropertiesFor(Configuration Scenario)
        {
            try
            {
                string type = Scenario.PropertyType;
                IList<Property> properties = RepoHelper.GetCurvesFor<Property>(context.Properties, type, Scenario);

                if (Scenario.ScenarioType.IsVA)
                {
                    return properties.Where(pr => pr.Value.Equals(properties.Max<Property>(p => p.Value))).ToList<Property>();
                }
                else
                {
                    return properties;
                }
            }
            catch { throw new ScenarioEntityException(ErroMessage("Properties", Scenario)); }
        }



        public NominalCurve GetNominalRatesFor(Configuration Scenario, string LiquidityLevel = "")
        {
            try
            {
                return RepoHelper.GetNominalRatesFor(context.NominalCurves, LiquidityLevel, Scenario);
            }
            catch (Exception e) { throw new ScenarioEntityException(ErroMessage("NominalRates " + LiquidityLevel, Scenario), e); }
        }

        public IList<ScenarioType> GetScenarioTypes(int year)
        {
            try
            {
                IList<ScenarioType> type = context.ScenarioTypes.Where<ScenarioType>(eq => eq.ScenarioDate.Year.Equals(year)).ToList<ScenarioType>();
                return type;

            }
            catch { throw new ScenarioEntityException("Cannot get Scenario Types " + year); }


        }
        public IList<BhcTemplate> GetTemplatesFor(Configuration Scenario)
        {
            try
            {
                string templType = RepoHelper.GetTypeTemplate(Scenario.ScenarioType.IsSwap);
                DateTime maxDate = context.BhcTemplates.Where(a => a.Date <= Scenario.ScenarioType.ReferenceScenarioDate).Max(d => d.Date);
                string Path = "";
                if (Scenario.ModelParameter.Models.IsOfType(Model.ModelType.LMMSVJD))
                {
                    Path = Scenario.ModelParameter.Models.Equity.ModelName;
                }
                var templates = context.BhcTemplates.Where<BhcTemplate>(
                    templ => templ.Date.Equals(maxDate)
                        && ((templ.Type.Equals(templType) && templ.Path.Contains(Path)) || templ.Type.Equals(Scenario.Economy))
                 ).ToList<BhcTemplate>();


                return templates;
            }
            catch { throw new ScenarioEntityException(ErroMessage("Templates", Scenario)); }
        }

        public IList<Credit> GetCreditsFor(Configuration Scenario)
        {
            var defaultCredits = RepoHelper.GetDefaultCredits(context.DefaultCredits.ToList(), Scenario);

            IList<Credit> credits = new List<Credit>();
            defaultCredits.ToList().ForEach(defaultCredit => credits.Add(
                new Credit()
                {
                    Spread = defaultCredit.Spread,
                    DefaultCredit = defaultCredit
                }));
            foreach (Credit credit in credits)
            {
                GetCTMFor(Scenario).ToList().ForEach(ctm => credit.CreditTransitionMatrices.Add(ctm));
            }

            return credits;
        }

        public IList<CreditTransitionMatrix> GetCTMFor(Configuration Scenario)
        {
            try
            {
                DateTime maxDate = context.CreditTransitionMatrices.Where(
                 a => a.Date <= Scenario.ScenarioType.ReferenceScenarioDate
                  && a.Type.Equals(Scenario.ScenarioType.TransitionMatrixType)
                 ).Max(d => d.Date);

                return context.CreditTransitionMatrices.Where<CreditTransitionMatrix>(
                    mat => mat.Date.Equals(maxDate)
                        && (mat.Type.Equals(Scenario.ScenarioType.TransitionMatrixType))
                 ).ToList<CreditTransitionMatrix>();
            }
            catch { throw new ScenarioEntityException(ErroMessage("Credt Transition Matrix", Scenario)); }
        }

        public int GetNextVersionFor(Configuration config)
        {
            var sameScenarios = context.Configurations.Where(c => c.ScenarioType.ID.Equals(config.ScenarioType.ID) &&
                c.Economy.Equals(config.Economy)).ToList();

            int version = sameScenarios.Max(ver => ver.Version);

            return (version / Configuration.ScenarioVersionFactor) * Configuration.ScenarioVersionFactor + Configuration.ScenarioVersionFactor;
        }

        public int GetNextSeedVersionFor(Configuration config)
        {
            var sameScenarios = context.Configurations.Where(c => c.ScenarioType.ID.Equals(config.ScenarioType.ID) &&
                c.Economy.Equals(config.Economy)).ToList();
            sameScenarios = sameScenarios.Where(s => s.Version / 100 == config.Version / 100).ToList();
            int version = sameScenarios.Max(ver => ver.Version);

            return (version / Configuration.ScenarioVersionFactor) * Configuration.ScenarioVersionFactor + version % Configuration.ScenarioVersionFactor + 1;
        }

        public IList<Model> GetModels()
        {
            try
            {
                return context.Models.ToList<Model>();
            }
            catch { throw new ScenarioEntityException("Cannot load Models"); }
        }


        public ModelsFactor GetFactorsFor(Configuration Scenario)
        {
            DateTime maxDate = context.ModelsFactors.Where(
                 a => a.Date <= Scenario.ScenarioType.ReferenceScenarioDate
                  ).Max(d => d.Date);
            int gid = context.ModelsFactors.Where(
                 a => a.Date == maxDate
                  ).Max(d => d.GID);

            return context.ModelsFactors.Where(m => m.GID == gid).First();
        }

        private string ErroMessage(string property, Configuration config)
        {
            return "Cannot get " + property + " " + config.ScenarioType.ScenarioDate + " - " + config.ScenarioType.ModelType + "_" + config.ScenarioType.Description + " " + config.Economy;
        }


    }
}
