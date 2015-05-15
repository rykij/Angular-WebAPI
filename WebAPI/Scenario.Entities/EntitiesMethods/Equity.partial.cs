using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class Equity : IScenarioCurve, ITypedScenarioCurve
    {
        public Equity Clone {
            get
            {
                return new Equity() { 
                    Idx = this.Idx, 
                    Value = this.Value, 
                    Maturity=this.Maturity, 
                    Type=this.Type, 
                    Date = this.Date,
                    Economy = this.Economy,
                    ID = ID
                };
            }
        }

        public double Time
        {
            get { return Maturity/12; }
        }
    }
}
