using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class NominalRate : IScenarioCurve
    {
        public NominalRate Clone
        {
            get
            {
                return new NominalRate() { 
                    Time = this.Time,
                    Value = this.Value,
                };
            }
        }
    }
}
