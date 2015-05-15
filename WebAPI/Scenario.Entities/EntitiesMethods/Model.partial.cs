using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{

    public abstract partial class Model
    {
        public enum ModelType
        {
            E2FBK,
            LMMPLUS,
            LMMSVJD,
            LMMPLUS_M,
            LMMSVJD_M
        }
        
        protected IDictionary<string, string> equityProperties = new Dictionary<string, string>()
        {
            {"s0","s0"},
            {"sinf","sinf"},
            {"a","a"}
        };

        protected IDictionary<string, string> exchangeProperties = new Dictionary<string, string>()
        {
            {"ExchangeRateVol","vol"},
            {"ExchangeRateStart","Start"},
            {"ExchangeRateMu","Mu"}
        };

        protected IDictionary<string, string> realProperties = new Dictionary<string, string>()
        {
            {"r0","r(0)"},
            {"m0","m(0)"},
            {"g","g"},
            {"m","Mu"},
            {"ra1","a1"},
            {"ra2","a2"},
            {"rs1","s1"},
            {"rs2","s2"}
        };

        public virtual bool IsOfType(ModelType Type)
        {
            return false;
        }

        public virtual string TypeDescription { get { return ""; } }

        public virtual IDictionary<string, string> SwaptionParameters { get { return null; } }
        public virtual IDictionary<string, string> BoundingParameters { get { return null; } }
        public virtual IDictionary<string, string> EquityParameters { get { return equityProperties; } }
        public virtual IDictionary<string, string> ExchangeParameters { get { return exchangeProperties; } }
        public virtual IDictionary<string, string> RealParameters { get { return realProperties; } }
        public virtual string TimeStepFrequency { get { return "Annual"; } }
        public virtual double TimeStepMultiply { get { return 1.0; } }
        public virtual double ModelledYears { get { return 120.0; } }
        public virtual bool IsMonthly { get { return false; } }
        public virtual bool IsAnnual { get { return !IsMonthly; } }
        public virtual bool IsBounded { get { return true; } }
        public virtual double DefaultCap { get { return 0.999; } }
        public virtual double DefaultFloor { get { return -1; } }
        public virtual double DefaultFreezeUpper { get { return 5; } }
        public virtual double DefaultFreezeLower { get { return -0.5; } }
        public virtual int DefaultBounding { get { return 0; } }
    }
}
