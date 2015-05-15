using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class InitialScenarioType : ScenarioType
    {
        public override bool IsInitial
        {
            get
            {
                return true;
            }
        }

        public override string ModelType
        {
            get
            {
                return "BH";
            }
        }
    }
}
