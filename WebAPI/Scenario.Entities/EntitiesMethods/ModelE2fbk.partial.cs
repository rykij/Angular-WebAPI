using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    
    public partial class ModelE2fbk : Model
    {
        protected readonly IDictionary<string, string> swaptionProperties = new Dictionary<string, string>()
        {
            {"na1","a1"},
            {"na2","a2"},
            {"ns1","s1"},
            {"ns2","s2"}
        };

        protected readonly IDictionary<string, string> boundingProperties = new Dictionary<string, string>()
        {
            {"nCap","CAP"}       
        };
        
        public override string TypeDescription { get { return Model.ModelType.E2FBK.ToString(); } }

        public override bool  IsOfType(ModelType Type){
            return Type.Equals(Model.ModelType.E2FBK);
        }

        public override IDictionary<string,string> SwaptionParameters { get { return swaptionProperties; } }
        public override IDictionary<string, string> BoundingParameters { get { return boundingProperties; } }
        public override bool IsBounded { get { return false; } }
    
    }
}
