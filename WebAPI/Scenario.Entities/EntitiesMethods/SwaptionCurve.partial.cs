using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class SwaptionCurve : ITypedScenarioCurve
    {
        public SwaptionCurve Clone {
            get {

                SwaptionCurve clone = new SwaptionCurve()
                {
                    Date = Date,
                    Economy = Economy,
                    Type = Type,
                    Swaptions = new List<Swaption>()
                };
                Swaptions.ToList().ForEach(s => clone.Swaptions.Add(new Swaption()
                {
                    Maturity = s.Maturity,
                    Time = s.Time,
                    Value = s.Value,
                    ID = s.ID
                }));

                return clone;
            }
        }
    }
}
