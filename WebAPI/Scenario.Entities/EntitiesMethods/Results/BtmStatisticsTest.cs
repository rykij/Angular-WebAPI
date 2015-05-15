using System.Collections.Generic;
namespace Scenario.Entities
{

    public class BmtStatisticTest : StatisticTest<BmtTestData>
    {
        public bool IsMartingalePercentilesTest { get { return Type.Equals("Bond") == false; } }
        public bool IsCeConvergencyTest { get { return Type.Equals("Cash"); } }
        public bool IsMartingalePerMaturityTest { get { return Type.Equals("Bond for single maturity") == true; } }
        public bool IsMartingaleAverage { get { return Type.Equals("Average") == true; } }
    }

    public class BmtTestData
    {
        public double Timestep;
        public double Maturity;
        public double Average;
        public double StdError;


        public BmtTestData Clone()
        {
            return new BmtTestData()
            {
                Timestep = this.Timestep,
                Average = this.Average,
                StdError = this.StdError,
                Maturity = this.Maturity
            };
        }
    }
}
