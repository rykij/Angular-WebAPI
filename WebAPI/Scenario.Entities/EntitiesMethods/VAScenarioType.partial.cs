using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class VAScenarioType : ScenarioType
    {
        public override string ModelType { get { return "VA"; } }

        public override bool IsVA
        {
            get
            {
                return true;
            }
        }
        
        public override string TypeForNominal
        {
            get
            {
                return new InitialScenarioType { Description = baseDescription }.TypeForNominal;
            }
        }
    }
}
