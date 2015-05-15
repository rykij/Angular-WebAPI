using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public interface IScenarioCurve
    {
        double Time { get; }
        double Value { get; }
    }
}
