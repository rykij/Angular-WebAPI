using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class Real : IScenarioCurve
    {
        public Real Clone
        {
            get
            {
                return new Real() { 
                    Time = this.Time,
                    Value = this.Value,
                };
            }
        }
    }
}
