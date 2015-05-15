using System.Collections.Generic;
namespace Scenario.Entities
{

    public class PricingTest : StatisticTest<PricingData>
    {
    }

    public class PricingData
    {
        public double Timestep;
        public double Tenor;
        public double Value;
        public double Strike;
        public double ErrorUp;
        public double ErrorDown;
    }
}
