using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class NominalCurve 
    {
        public NominalCurve Clone
        {
            get
            {
                NominalCurve clone =  new NominalCurve() { 
                    Date = Date,
                    Economy = Economy,
                    GID = GID,
                    NominalRates = new List<NominalRate>()
                };
                NominalRates.ToList().ForEach(n=>clone.NominalRates.Add(new NominalRate(){
                    Id = n.Id,
                    Time = n.Time,
                    Value = n.Value
                }));
                return clone;
            }
        }
    }
}
