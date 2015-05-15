using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class SESScenarioType : ScenarioType
    {
        public override string ModelType { get { return "SES"; } }
        
        public override string TypeForNominal
        {
            get
            {
                return TypeForEquity;
            }
        }

        public override string TypeForSwaption
        {
            get
            {
                return TypeForEquity;
            }
        }

        public override string TypeForEquity
        {
            get
            {
                string type = ModelType + "_" + Description;
               
                return type;
            }
        }
        public override bool IsSES { get { return true; } }
    }
}
