using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class EBSScenarioType : ScenarioType
    {
        public override bool IsEBS
        {
            get
            {
                return true;
            }
        }

        public override string ModelType { get { return "EBS"; } }
        public override string TypeForInflation
        {
            get
            {
                return ModelType + "_";
            }
        }
    }
}
