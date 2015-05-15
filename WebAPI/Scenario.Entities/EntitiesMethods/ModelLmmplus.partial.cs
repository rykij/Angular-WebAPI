using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    
    public partial class ModelLmmplus : Model 
    {
        protected readonly IDictionary<string, string> swaptionProperties = new Dictionary<string, string>()
        {
            {"LMMPlus_a","a"},
            {"LMMPlus_b","b"},
            {"LMMPlus_c","c"},
            {"LMMPlus_d","d"},
            {"LMMPlusVolInit","Vol Init"},
            {"LMMPlusRevRate","Rev Rate"},
            {"LMMPlusRevLevel","Rev Level"},
            {"LMMPlusVol","Vol"},
            {"LMMPlusVolCorr","Vol Corr"},
            {"LMMPlusRateDispl","Rate Displ"}
        };

        protected readonly IDictionary<string, string> boundingProperties = new Dictionary<string, string>()
        {
            {"nCap","CAP"},
            {"nFloor","Floor"},
            {"nFreezeUpper","Freeze U"},
            {"nFreezeLower","Freeze L"},
            {"nBounding","Bounding"},
        };

        public override bool IsOfType(ModelType Type)
        {
            return Type.Equals(Model.ModelType.LMMPLUS);
        }

        public override string TypeDescription { get { return Model.ModelType.LMMPLUS.ToString(); } }

        public override IDictionary<string, string> SwaptionParameters { get { return swaptionProperties; } }
        public override IDictionary<string, string> BoundingParameters { get { return boundingProperties; } }
        public override double DefaultCap { get { return 1.71; } }
    }
}
