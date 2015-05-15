using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public class ScenarioAnalysisTest
    {
        public string Type;
        public string PayoffType;
        public string Economy;
        public double Strike;
        public double Tenor;
        public IList<ScenarioAnalysisData> Prices;
    }

    public class ScenarioAnalysisData
    {
        public double Maturity;
        public double EstimatedPrice;
        public double StdError;
        public double ExpectedPrice;
        public double ImpliedVol;
        public double ImpliedVolUp;
        public double ImpliedVolLow;
    }
}
