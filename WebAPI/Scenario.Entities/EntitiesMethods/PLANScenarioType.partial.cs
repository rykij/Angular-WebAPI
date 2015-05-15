using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class PLANScenarioType : ScenarioType
    {
        public override string ModelType { get { return "PLAN"; } }
        public override string TypeForInflation
        {
            get
            {
                string description = Description.Split('_')[0] + "_";
                return ModelType + "_" + description;
            }
        }
    }
}
