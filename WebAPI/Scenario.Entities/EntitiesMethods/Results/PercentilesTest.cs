using System.Collections.Generic;
namespace Scenario.Entities
{

    public class PercentilesTest : StatisticTest<PercentilesData>
    {
    }

    public class PercentilesData
    {
        public double Timestep;
        public double Value;
        public double Percentile;
    }
}
