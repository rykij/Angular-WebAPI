using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public interface ISwaption
    {
        double Maturity { get; }
        double Value { get; }
        double Time { get; }
    }
}