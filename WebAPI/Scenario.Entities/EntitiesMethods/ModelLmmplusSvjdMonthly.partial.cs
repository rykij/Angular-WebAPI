﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{

    public partial class ModelLmmplusSvjdMonthly : ModelLmmplusSvjd
    {
        public override bool IsOfType(ModelType Type)
        {
            return Type.Equals(Model.ModelType.LMMSVJD_M);
        }

        public override string TypeDescription
        {
            get
            {
                // al fine di alberatura e folder è un LMMSVJD
                return Model.ModelType.LMMSVJD.ToString();
            }
        }

        public override string TimeStepFrequency { get { return "Monthly"; } }
        public override double TimeStepMultiply { get { return 12.0; } }
        public override bool IsMonthly { get { return true; } }
    }
}
