using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class Property : ITypedScenarioCurve
    {
        public Property Clone
        {
            get { return new Property() { 
                Idx = this.Idx, 
                Value = this.Value,
                Date = this.Date,
                Type = this.Type,
                Economy = this.Economy
            }; 
            }
        }

        public double Time { get { return Idx; } }
    }
}
