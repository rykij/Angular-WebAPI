using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public class ScenarioElaborationTest
    {
        public IList<ScenarioAnalysisTest> AnalysisTest = new List<ScenarioAnalysisTest>();
        public IList<BmtStatisticTest> StatisticTest = new List<BmtStatisticTest>();
    }
}
