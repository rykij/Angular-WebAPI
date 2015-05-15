using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class EIOPAScenarioType : ScenarioType
    {
        public override string ModelType { get { return "EIOPA"; } }
    }
}
