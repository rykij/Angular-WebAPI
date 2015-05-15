using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class Credit
    {
        public Credit Clone
        {
            get
            {
                Credit clone = new Credit()
                {
                    Pi = this.Pi,
                    Spread = this.Spread,
                    Price = this.Price,
                    SpreadAdj = this.SpreadAdj,
                    DefaultCredit = this.DefaultCredit
                };

                clone.CreditTransitionMatrices = this.CreditTransitionMatrices;

                return clone;
            }
        }
    }
}
