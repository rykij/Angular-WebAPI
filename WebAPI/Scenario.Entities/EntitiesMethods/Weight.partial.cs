using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class Weight : ISwaption
    {
        public Weight Clone
        {
            get
            {
                return new Weight() { Maturity = this.Maturity, Value = this.Value, Time = this.Time };
            }
        }
    }
}
