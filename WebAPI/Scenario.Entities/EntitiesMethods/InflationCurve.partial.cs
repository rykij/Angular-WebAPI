using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class InflationCurve : ITypedScenarioCurve
    {
        public InflationCurve Clone {
            get { 
                InflationCurve clone = new InflationCurve(){
                    Date = Date,
                    Economy = Economy,
                    Type = Type,
                    Inflations = new List<Inflation>()
                };

                Inflations.ToList().ForEach(i => clone.Inflations.Add(new Inflation()
                {
                    Id = i.Id,
                    Time = i.Time,
                    Value = i.Value
                }));

                return clone;
            }
        }
    }
}
