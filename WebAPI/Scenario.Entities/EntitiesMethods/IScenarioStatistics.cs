using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Scenario.Entities
{
    public interface IScenarioStatistics
    {
        IList<BmtStatisticTest> Martingale { get;  }
        IList<PricingTest> Pricing { get;  }
        IList<PercentilesTest> Percentiles { get;  }
        IList<CorrelationsTest> Correlations { get; }
        IList<MomentsTest> Moments { get; }
        IList<AveragesTest> Averages { get; }
        IList<AveragesTest> Inputs { get; }

        void RunAnalysis();
    }
}
