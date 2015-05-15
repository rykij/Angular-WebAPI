using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class MCEVScenarioType : ScenarioType
    {
        public override string ModelType { get { return "MCEV"; } }
        protected readonly string noipDescription = "NOIP";
        public override string ReferenceBaseType { get { return ModelType; } }
        public override string TypeForInflation
        {
            get
            {
                return ModelType + "_";
            }
        }
    }
}
