using System;
using System.Collections.Generic;
using System.Linq;
using Scenario.Entities;

namespace Scenario.Repository
{
    public class DesignTimeScenarioRepository : IScenarioRepository
    {
        static IDictionary<int,Configuration> scenarios;
        public DesignTimeScenarioRepository() {
            scenarios = new Dictionary<int,Configuration>();
            int counter = 0;
            for (int i = 0; i < 2; i++)
            {
                Configuration conf = GetNewConf(counter++);
                
                scenarios.Add(conf.ID,conf);
            }
            Configuration c = GetNewConf(counter++);
            c.Version++;
            c.ScenarioType.Description = "test " + (counter-2);
            scenarios.Add(c.ID,c);

            Configuration multiEconomy = GetNewConf(counter++,"CHF");
            Configuration child = GetNewConf(counter++);
            multiEconomy.Children.Add(child);
            scenarios.Add(multiEconomy.ID, multiEconomy);
        }

        private static Configuration GetNewConf(int i, string Economy = "EUR")
        {
            ScenarioType stype = new EBSScenarioType()
            {
                Description = "test " + i,
                ScenarioDate = DateTime.Now.Date,
                Economy = "EUR",
                DefaultModel = new ModelE2fbk() { NYC = new ModelTemplate(), RYC = new ModelTemplate(), Property = new ModelTemplate(), Credit = new ModelTemplate(), Equity = new ModelTemplate() }
            };
            Configuration conf = new Configuration()
            {
                Economy = Economy,
                ID = i,
                status = 0,
                ScenarioType = stype
            };
            conf.ModelParameter = new ModelParameter()
            {
                a = 0.01,
                s0 = 1,
                sinf = 2,
                r0 = 3,
                m0 = 4,
                m = 5,
                sesReal1 = 0,
                sesReal2 = 0,
                Models = new ModelLmmplus()

            };
            conf.Seed = 1;
            conf.ScenarioType.DefaultModel.ModelParameters.Add(conf.ModelParameter);
            conf.Credits.Add(new Credit() { Pi = 1, Price = 2 });
            conf.InflationCurveCe = new InflationCurve();
            conf.InflationCurve = new InflationCurve();
            conf.NominalCurve = new NominalCurve();
            for (int j = 1; j <= 120; j++)
            {
                if(j<=80)
                    conf.InflationCurveCe.Inflations.Add(new Inflation() { Time = j, Value = 1 / (0.99-j*0.001) - 1 });
                conf.InflationCurve.Inflations.Add(new Inflation() { Time = j, Value = 1 / (0.99 - j * 0.001) - 1 });
                conf.NominalCurve.NominalRates.Add(new NominalRate() { Time = 1, Value = 0.99 - j*0.01 });
            }
            return conf;
        }

        public IList<IResult> GetResults(Configuration Scenario, params int[] Trials)
        {
            return Scenario.Results;
        }

        public void Save(Configuration config)
        {
            if(scenarios.Keys.Contains(config.ID))
                scenarios[config.ID] = config;
            else
            scenarios.Add(config.ID,config);
        }

        public void SaveCreditFor(Configuration Scenario)
        {
            if (scenarios.Keys.Contains(Scenario.ID))
            {
                for (int i = 0; i < Scenario.Credits.Count; i++)
                {
                    scenarios[Scenario.ID].Credits.ElementAt(i).Pi = Scenario.Credits.ElementAt(i).Pi;
                    scenarios[Scenario.ID].Credits.ElementAt(i).Price = Scenario.Credits.ElementAt(i).Price;
                    scenarios[Scenario.ID].Credits.ElementAt(i).Spread = Scenario.Credits.ElementAt(i).Spread;
                    scenarios[Scenario.ID].Credits.ElementAt(i).DefaultCredit = Scenario.Credits.ElementAt(i).DefaultCredit;
                }
            }
        }

        public IList<Configuration> GetAll(bool OnlyMainEconomy = true)
        {
            if(OnlyMainEconomy)
                return scenarios.Values.Where(s => s.Parent == null).ToList();
            else
                return scenarios.Values.ToList();
        }

        public void Delete(Configuration config)
        {
            ;
        }

        public void DeleteCredits(Configuration config)
        {
            ;
        }

        public Configuration Load(int configId)
        {
            return scenarios[configId];
        }

        public Configuration Get(int configId)
        {
            return Load(configId);
        }

        public Configuration GetBaseFor(Configuration Config, bool SameDate)
        {
            return null;
        }

        public Configuration Get(ScenarioType type)
        {
            return Load(0);
        }

        public IList<Equity> GetEquitiesFor(Configuration config) {
            return new List<Equity>();
        }
        public SwaptionCurve GetSwaptionsFor(Configuration config)
        {
            return new SwaptionCurve();
        }
        public SwaptionCurve GetMarketSwaptionsFor(Configuration config)
        {
            return new SwaptionCurve();
        }
        public IList<Property> GetPropertiesFor(Configuration config)
        {
            return new List<Property>();
        }
        public InflationCurve GetInflationsFor(Configuration config)
        {
            return new InflationCurve();
        }
        public InflationCurve GetCertaintyEquivalentInflationsFor(Configuration config)
        {
            return new InflationCurve();
        }
        public NominalCurve GetNominalRatesFor(Configuration config, string LiquidityPremium = "")
        {
            return new NominalCurve();
        }
        public IList<ScenarioType> GetScenarioTypes(int year)
        {
            return new List<ScenarioType>();
        }
        public IList<BhcTemplate> GetTemplatesFor(Configuration config) {
            return new List<BhcTemplate>();
        }

        public IList<CreditTransitionMatrix> GetCTMFor(Configuration config)
        {
            return new List<CreditTransitionMatrix>();
        }

        public int GetNextVersionFor(Configuration config) {
            return 1;
        }
        public int GetNextSeedVersionFor(Configuration config)
        {
            return 1;
        }
        public void ClearResultsFor(Configuration Scenario, int UpToTiral=0) {
            ;
        }

        public IList<Credit> GetCreditsFor(Configuration Scenario) {
            return new List<Credit>();
        }

        public IList<Factor> GetModelFactors(Configuration Scenario) {
            return new List<Factor>();
        }

        public IList<Model> GetModels()
        {
            return new List<Model>();
        }



        public IList<Configuration> GetScenariosOfType(ScenarioType Type)
        {
            throw new NotImplementedException();
        }


        public ModelsFactor GetFactorsFor(Configuration Scenario)
        {
            throw new NotImplementedException();
        }


        public TestWeight GetTestWeightsFor(Configuration Scenario)
        {
            throw new NotImplementedException();
        }


        IList<ScenarioSummary> IScenarioRepository.GetAll(bool OnlyMainEconomy)
        {
            IList<ScenarioSummary> summary = new List<ScenarioSummary>();
            foreach(KeyValuePair<int,Configuration> scenario in scenarios) {
                summary.Add(new ScenarioSummary()
                {
                    Country = scenario.Value.Country,
                    Economy = scenario.Value.Economy,
                    TypeEconomy = scenario.Value.ScenarioType.Economy,
                    Version = scenario.Value.Version,
                    Date = scenario.Value.ScenarioType.ScenarioDate,
                    Description = scenario.Value.ScenarioType.Description,
                    ID = scenario.Value.ID,
                    ParentId = scenario.Value.ParentID,
                    Type = scenario.Value.ScenarioType.ModelType,
                    TypeDescription = scenario.Value.ModelParameter.Models.TypeDescription,
                    Status = scenario.Value.status
                });
            }

            return summary;
        }
    }
}
