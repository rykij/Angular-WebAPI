using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Repository
{
    public class ExtendedTypedCurve : TypedCurve
    {
        public string LiquidityLevel
        {
            get;
            set;
        }
        
        public double Time
        {
            get;
            set;
        }
    }
}
