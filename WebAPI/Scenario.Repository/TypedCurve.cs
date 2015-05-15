using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Repository
{
    public class TypedCurve : Entities.ITypedScenarioCurve
    {
        public DateTime Date
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Economy
        {
            get;
            set;
        }
    }
}
