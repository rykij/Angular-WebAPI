using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class SSTScenarioType : ScenarioType
    {
        public override string ModelType { get { return "SST"; } }
        public override string ReferenceBaseType { get { return ModelType; } }
        public override bool IsSST { get { return true; } }
        public override string TypeForInflation
        {
            get
            {
                return ModelType + "_";
            }
        }

        public override string TypeForProperty
        {
            get
            {
                return TypeForInflation;
            }
        }

        public override string BaseTypeForSwaption
        {
            get { return TypeForInflation + baseDescription; }
        }

        public override string TypeForEquity
        {
            get
            {
                string type = ModelType + "_" + Description;
                if (IsEIV == false)
                {
                    type = TypeForInflation + baseDescription;
                }
                return type;
            }
        }
    }
}
