using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{

    public partial class ModelLmmplusSvjd : ModelLmmplus
    {
        public ModelLmmplusSvjd() : base() {

            equityProperties = new Dictionary<string, string>()
            {
                {"SVJDVolInit","Init Vol"},
                {"SVJDRevLevel","Rev Level"},
                {"SVJDRevSpeed","Rev Speed"},
                {"SVJDVol","Vol Volatility"},
                {"SVJDCOrr","Corr"},
                {"SVJDJumpArrivalRate","Jump Arr Rate"},
                {"SVJDJumpMean","Jump Mean"},
                {"SVJDJumpVol","Jump Vol"}
            };
        }

        public override bool IsOfType(ModelType Type)
        {
            return Type.Equals(Model.ModelType.LMMSVJD);
        }

        public override string TypeDescription { get { return Model.ModelType.LMMSVJD.ToString(); } }

        public override IDictionary<string, string> EquityParameters { get { return equityProperties; } }
    }
}
