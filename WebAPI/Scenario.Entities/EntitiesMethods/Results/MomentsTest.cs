using System.Collections.Generic;
namespace Scenario.Entities
{

    public class MomentsTest : StatisticTest<MomentsData>
    {
    }

    public class MomentsData
    {
        public double Timestep;
        public double Avg;
        public double StdDev;
        public double Skew;
        public double Kurt;
    }
}
