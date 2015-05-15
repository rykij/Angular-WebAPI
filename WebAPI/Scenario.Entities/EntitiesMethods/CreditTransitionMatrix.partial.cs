using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class CreditTransitionMatrix
    {
        public CreditTransitionMatrix Clone {
            get {
                return new CreditTransitionMatrix()
                {
                    StartRating = this.StartRating,
                    EndRating = this.EndRating,
                    Type = this.Type,
                    Date = this.Date,
                    Value = this.Value
                };
            }
        }
    }
}
