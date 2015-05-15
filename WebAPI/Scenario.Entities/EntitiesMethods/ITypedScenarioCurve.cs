using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public interface ITypedScenarioCurve
    {
        DateTime Date { get; }
        string Type { get; }
        string Economy { get; }
    }
}
