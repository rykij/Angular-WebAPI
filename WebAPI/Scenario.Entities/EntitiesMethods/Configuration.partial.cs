using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Scenario.Entities
{

    public partial class Configuration : ConfigurationBase
    {
        protected int rollbackStatus = -1;


        public int MainVersion
        {
            get { return GetMainVersion(Version); }
        }

        public int MinVersion
        {
            get { return GetMinVersion(Version); }
        }

        public string Country
        {
            get
            {
                if (ScenarioType != null)
                    return ScenarioType.Country;
                return "";
            }
        }

        public Configuration BaseForPreCalibrationParams { get; set; }
        public IScenarioStatistics ScenarioStatistics { get; set; }

        public ScenarioElaborationTest Test { get; set; }

        public ScenarioCertainEquivalent CertainEquivalentScenario { get; set; }

        public IList<IResult> Results { get; set; }

        public int Bonds
        {
            get
            {
                if (Economy == "HUF" || Economy == "CZK")
                    return 1000;
                return 10000;
            }
        }
        public int DefaultTimeStep
        {
            get
            {
                if (ScenarioType.Economy == "HUF" || ScenarioType.Economy.Contains("CHF"))
                    return 40;
                return 60;
            }
        }

        public IReleaseConfigurationActionInfo ReleaseScenarioInfo { get; set; }

        public string PropertyType
        {
            get
            {
                string multiplicity = "Single";
                if (Parent != null || Children.Count > 0)
                {
                    multiplicity = "Multi";
                }

                return ScenarioType.TypeForProperty + multiplicity;
            }
        }

        public ICollection<NominalRate> NominalRates
        {
            get
            {
                if (NominalCurve != null)
                    return NominalCurve.NominalRates.ToList().AsReadOnly();
                return new List<NominalRate>().AsReadOnly();

            }
        }

        public ICollection<TestWeigthPoint> TestWeights
        {
            get
            {
                if (TestWeight1 != null)
                    return TestWeight1.TestWeigthPoints.ToList().AsReadOnly();
                return new List<TestWeigthPoint>().AsReadOnly();

            }
        }

        public ICollection<NominalRate> NominalRatesLP0
        {
            get
            {
                if (NominalCurveLp0 != null)
                    return NominalCurveLp0.NominalRates.ToList().AsReadOnly();
                return new List<NominalRate>().AsReadOnly(); ;
            }
        }

        public ICollection<Inflation> InflationsCe
        {
            get
            {

                if (InflationCurveCe != null)
                    return InflationCurveCe.Inflations.ToList().AsReadOnly();
                return new List<Inflation>().AsReadOnly(); ;
            }
        }

        public ICollection<Inflation> AnnualInflationsCe
        {
            get
            {
                return InflationsCe.Where(nn => nn.Time % ModelParameter.Models.TimeStepMultiply == 0).OrderBy(n => n.Time).ToList<Inflation>();
            }
        }

        public ICollection<NominalRate> AnnualNominalRates
        {
            get
            {
                return NominalRates.Where(nn => nn.Time % ModelParameter.Models.TimeStepMultiply == 0).OrderBy(n => n.Time).ToList<NominalRate>();
            }
        }


        public ICollection<Inflation> Inflations
        {
            get
            {
                if (InflationCurve != null)
                    return InflationCurve.Inflations.ToList().AsReadOnly(); ;
                return new List<Inflation>().AsReadOnly(); ;
            }
        }

        public ICollection<Inflation> AnnualInflations
        {
            get
            {
                return Inflations.Where(nn => nn.Time % ModelParameter.Models.TimeStepMultiply == 0).OrderBy(n => n.Time).ToList<Inflation>();
            }
        }


        public ICollection<Swaption> MarketSwaptions
        {
            get
            {
                if (SwaptionMarketCurve != null)
                    return SwaptionMarketCurve.Swaptions.ToList().AsReadOnly(); ;
                return new List<Swaption>().AsReadOnly(); ;
            }
        }

        public ICollection<Swaption> Swaptions
        {
            get
            {
                if (SwaptionCurve != null)
                    return SwaptionCurve.Swaptions.ToList().AsReadOnly(); ;
                return new List<Swaption>().AsReadOnly(); ;
            }
        }

        public Configuration GetCurvesSnapshot()
        {
            Configuration curveSnapshot = new Configuration();
            if(NominalCurve != null)
                curveSnapshot.NominalCurve = NominalCurve.Clone;
            if (NominalCurveLp0 != null)
                curveSnapshot.NominalCurveLp0 = NominalCurveLp0.Clone;
            curveSnapshot.Equities = new List<Equity>();
            Equities.ToList().ForEach(e=>curveSnapshot.Equities.Add(e.Clone));
            if (InflationCurve != null)
                curveSnapshot.InflationCurve = InflationCurve.Clone;
            if (InflationCurveCe != null)
                curveSnapshot.InflationCurveCe = InflationCurveCe.Clone;
            if (SwaptionCurve != null)
                curveSnapshot.SwaptionCurve = SwaptionCurve.Clone;
            if (SwaptionMarketCurve != null)
                curveSnapshot.SwaptionMarketCurve = SwaptionMarketCurve.Clone;

            return curveSnapshot;
        }

        public Configuration Clone
        {
            get
            {
                Configuration clone = new Configuration();
                clone.ScenarioType = ScenarioType;
                clone.Economy = Economy;
                clone.Version = Version;
                clone.Seed = Seed;
                clone.Simulated = Simulated;
                clone.Trials = Trials;
                clone.Timestep = Timestep;
                clone.BaseScenario = BaseScenario;
                clone.CalibrationDescription = CalibrationDescription;

                clone.NominalCurve = NominalCurve;
                clone.NominalCurveLp0 = NominalCurveLp0;
                clone.Equities = Equities;
                clone.InflationCurve = InflationCurve;
                clone.InflationCurveCe = InflationCurveCe;
                clone.Properties = Properties;
                clone.SwaptionCurve = SwaptionCurve;
                clone.SwaptionMarketCurve = SwaptionMarketCurve;
                
                this.Reals.ToList().ForEach(x => clone.Reals.Add(x.Clone));
                this.Weights.ToList().ForEach(x => clone.Weights.Add(x.Clone));
                this.Credits.ToList().ForEach(x => clone.Credits.Add(x.Clone));

                if (this.ModelParameter != null)
                {
                    clone.ModelParameter = this.ModelParameter.Clone;
                    clone.ModelParameter.Configuration = clone;
                }

                Children.ToList().ForEach(ch => clone.Children.Add(ch.Clone));


                return clone;
            }
        }

        public void CleanCurves()
        {
            NominalCurve = null;
            NominalCurveLp0 = null;
            InflationCurve = null;
            InflationCurveCe = null;
            SwaptionCurve = null;
            SwaptionMarketCurve = null;
            TestWeight1 = null;
            Equities = null;
            Properties = null;
            BhcTemplates = null;
        }

        public string ModelTypeIdentifyer
        {
            get
            {
                return ScenarioType.ScenarioDate.ToString("yyyyMMdd") +
                    ScenarioType.Economy + ScenarioType.Description + ScenarioType.Country + ModelParameter.Models.Id + ScenarioType.ModelType;
            }
        }

        public string Identifyer
        {
            get
            {
                string version = MainVersion + (MinVersion != 0 ? "." + MinVersion : "");
                return ScenarioType.ScenarioDate.ToString("yyyyMMdd") + "." + ScenarioType.Economy + "." + ScenarioType.Country
                   + "." + ModelParameter.Models.TypeDescription + "." + ScenarioType.ModelType + "." + ScenarioType.Description + "." + version;
            }
        }

        public string Path
        {
            get
            {
                return Identifyer.Replace(".", "\\").Replace('_', ' ');
            }
        }

        public string PublishPath
        {
            get
            {
                string path = Path;
                string root = path.Split('\\')[0];
                string newRoot = YearMonthPath + "\\Scenari";
                string publishPath = path.Replace(root, newRoot);
                return publishPath;
            }
        }

        public string YearMonthPath
        {
            get
            {
                string month = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(ScenarioType.ScenarioDate.Month).Substring(0, 3);
                return "End" + month + ScenarioType.ScenarioDate.Year;
            }
        }

        public bool IsElaborating
        {
            get
            {
                return IsStateAll(Status.ELABORATING);
            }
        }

        public void SetElaborating()
        {
            SetStateAll(Status.ELABORATING);
        }

        public bool IsElaborated
        {
            get
            {
                return IsStateAll(Status.ELABORATED);
            }
        }

        public void SetElaborated()
        {
            SetStateAll(Status.ELABORATED);
        }

        public void RollbackState()
        {
            foreach (Configuration c in this.SelfAndChildren)
            {
                if (c.rollbackStatus > -1)
                    c.status = c.rollbackStatus;
            }
        }

        public bool IsCalibrating
        {
            get
            {
                return IsStateOneOfAll(Status.CALIBRATING);
            }
        }

        public void SetCalibrating()
        {
            SetState(Status.CALIBRATING);
        }

        public bool IsCalibrated
        {
            get
            {
                return IsStateAll(Status.CALIBRATED);
            }
        }

        public void SetCalibrated()
        {
            SetStateAll(Status.CALIBRATED);
        }

        public bool IsPublished
        {
            get
            {
                return IsStateAll(Status.PUBLISHED);
            }
        }

        public void SetPublished()
        {
            SetStateAll(Status.PUBLISHED);
        }

        public bool IsArchived
        {
            get
            {
                return IsStateAll(Status.ARCHIVED);
            }
        }

        public void SetArchived()
        {
            SetStateAll(Status.ARCHIVED);
        }

        public bool IsReleased
        {
            get
            {
                return IsStateAll(Status.RELEASED);
            }
        }

        public void SetReleased()
        {
            SetStateAll(Status.RELEASED);
        }

        public bool IsReady
        {
            get
            {
                return IsStateAll(Status.READY);
            }
        }

        public void SetReady()
        {
            SetStateAll(Status.READY);
        }

        public IList<SwaptionsForCheck> SwaptionsForChecks
        {
            get
            {
                IList<SwaptionsForCheck> checks = new List<SwaptionsForCheck>();
                foreach (Swaption s in MarketSwaptions)
                {
                    if (s.Value != 0)
                        checks.Add(new SwaptionsForCheck() { Maturity = s.Maturity, Tenor = s.Time });
                }
                return checks;
            }
        }

        public bool IsMainEconomy
        {
            get { return Parent == null; }
        }

        public bool LoadSimulated
        {
            get { return Simulated == 1 || ScenarioType.IsSES; }
        }

        public double GetWeightDefaultValue(int Maturity, int Time)
        {
            double? val = 1;
            if (BaseScenario != null && BaseScenario.ScenarioType.GetType().Equals(ScenarioType.GetType()))
            {
                try
                {
                    val = BaseScenario.Weights.Where(w => w.Time == Time && w.Maturity == Maturity).First().Value;
                }
                catch { val = 1; }
            }
            else
                val = Maturity <= 2 && Time <= 2 ? 0 : 1;

            return (double)val;
        }

        public void SetDefaultModelParameter()
        {
            if (BaseScenario != null)
            {
                BaseScenario.ModelParameter.CopyValues(ModelParameter);
                Reals.Clear();
                BaseScenario.Reals.ToList().ForEach(r=> Reals.Add(r.Clone));
            }
            if (ScenarioType.IsEIV)
            {
                ModelParameter.a = ModelParameter.s0 = ModelParameter.sinf = 0;
                ModelParameter.UnsetCalibration(Entities.ModelParameter.CalibrationType.EQUITY);
            }
            else if (ScenarioType.IsSIV)
            {
                ModelParameter.UnsetCalibration(Entities.ModelParameter.CalibrationType.SWAPTIONS);
                ResetNominalCalibration();
            }
            else
            {
                ResetNominalCalibration();
                ModelParameter.UnsetCalibration(Entities.ModelParameter.CalibrationType.SWAPTIONS);
                ModelParameter.UnsetCalibration(Entities.ModelParameter.CalibrationType.CREDIT);
                ModelParameter.UnsetCalibration(Entities.ModelParameter.CalibrationType.REAL);
                ModelParameter.UnsetCalibration(Entities.ModelParameter.CalibrationType.CERTEQUIVALENT);
            }

            if (BaseScenario == null)
            {
                if (Parent != null)
                {
                    ModelParameter.ExchangeRateMu = 0;
                    ModelParameter.ExchangeRateStart = 0;
                    ModelParameter.ExchangeRateVol = 0;
                }
                else
                {
                    ModelParameter.ExchangeRateMu = ModelParameter.ExchangeRateDefault;
                    ModelParameter.ExchangeRateStart = ModelParameter.ExchangeRateDefault;
                    ModelParameter.ExchangeRateVol = ModelParameter.ExchangeRateDefault;
                }
            }
        }

        private void ResetNominalCalibration()
        {
            ModelParameter.LMMPlus_a = 0;
            ModelParameter.LMMPlus_b = 0;
            ModelParameter.LMMPlus_c = 0;
            ModelParameter.LMMPlus_d = 0;
            ModelParameter.LMMPlusRateDispl = BaseScenario != null ? BaseScenario.ModelParameter.LMMPlusRateDispl : ScenarioType.DefaultRateDisplacement;
            ModelParameter.LMMPlusRevLevel = 0;
            ModelParameter.LMMPlusRevRate = 0;
            ModelParameter.LMMPlusVol = 0;
            ModelParameter.LMMPlusVolCorr = 0;
            ModelParameter.LMMPlusVolInit = 0;
            ModelParameter.na1 = 0;
            ModelParameter.na2 = 0;
            ModelParameter.ns1 = 0;
            ModelParameter.ns2 = 0;
        }

        public double GetDefaultSeed()
        {
            if (BaseForPreCalibrationParams != null)
                return BaseForPreCalibrationParams.Seed;
            return 0;
        }

        public double GetDefaultCap()
        {
            return ModelParameter.Models.DefaultCap;
        }

        public double GetDefaultFloor()
        {
            return ModelParameter.Models.DefaultFloor;
        }

        public IList<Configuration> SelfAndChildren
        {
            get
            {
                IList<Configuration> list = new List<Configuration>();
                list.Add(this);
                foreach (Configuration child in Children)
                    list.Add(child);
                return list;
            }
        }

        private void SetStateAll(Status Status)
        {
            foreach (Configuration conf in SelfAndChildren)
            {
                conf.SetState(Status);
            }
        }

        private void SetState(Status Status)
        {
            if (this.status.Equals((int)Status.CALIBRATING) == false && this.status.Equals((int)Status.ELABORATING) == false)
            {
                rollbackStatus = status;
            }
            status = (int)Status;
        }

        private bool IsStateAll(Status Status)
        {
            bool isState = true;
            foreach (Configuration conf in SelfAndChildren)
            {
                isState = isState & conf.status.Equals((int)Status);
            }
            return isState;
        }

        private bool IsStateOneOfAll(Status Status)
        {
            bool isState = false;
            foreach (Configuration conf in SelfAndChildren)
            {
                isState = isState | conf.status.Equals((int)Status);
            }
            return isState;
        }
    }
}