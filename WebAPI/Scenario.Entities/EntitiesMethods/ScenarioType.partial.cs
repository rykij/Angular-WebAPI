using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public abstract partial class ScenarioType
    {
        public static readonly char EconomySeparator = '-';
        public virtual bool IsSwap { get { return Swap > 0 ; } }
        public virtual bool IsInitial { get { return false; } }
        public virtual bool IsVA { get { return false; } }
        public virtual bool IsMcev { get { return false; } }
        public virtual bool IsNBV { get { return false; } }
        public virtual bool IsEBS { get { return false; } }
        public virtual bool IsSES { get { return false; } }
        public virtual bool IsSST { get { return false; } }
        public virtual bool IsSIV { get { return Description.Contains("SIV"); } }
        public virtual bool IsEIV { get { return Description.Contains("EIV"); } }
        public virtual bool IsDE { get { return Country.Equals("DE"); } }
        public virtual bool IsCzk { get { return Economy.Equals("CZK"); } }
        public virtual bool IsIT { get { return Country.Equals("IT"); } }
        public virtual bool IsBase { get { return Description.Contains(baseDescription); } }
        public virtual string ModelType {get {return "";}}
        public virtual DateTime ReferenceScenarioDate { get {return ScenarioDate;}}
        protected virtual string baseDescription { get { return "BASE"; } }
        public virtual string ReferenceBaseType {get {return "EBS";}}
        
        public virtual string LiquidityLevel
        {
            get
            {
                return string.Empty;
            }
        }

        public  virtual string TypeForNominal{
            get
            {
                string description = Description;
                if (IsSIV || IsEIV)
                    description = baseDescription;
                return ModelType + "_" + description ;
            }
        }

        public virtual string BaseTypeForSwaption {
            get { return baseDescription; }
        }

        public virtual string TypeForTestWeights
        {
            get { return ModelType; }
        }

        public virtual string BaseTypeForTestWeights
        {
            get { return baseDescription; }
        }

        public virtual string TypeForDefaultCredit
        {
            get
            {
                return ModelType + "_" + Description;
            }
        }

        public virtual string BaseTypeForDefaultCredit
        {
            get { return ModelType + "_" + baseDescription; }
        }

        public virtual string TypeForSwaption{
            get
            {
                string type = ModelType + "_" + Description;
                if (IsSIV == false)
                {
                    type = ModelType + "_" + baseDescription;
                }
                return type;
            }
        }

        public virtual string TypeForEquity
        {
            get
            {
                string type = ModelType + "_" + Description;
                if (IsEIV == false)
                {
                    type = baseDescription;
                }
                return type;
            }
        }

        public virtual string TypeForInflation
        {
            get
            {
                return "";
            }
        }
        public virtual string TypeForProperty
        {
            get
            {
                return "";
            }
        }

        public virtual double DefaultRateDisplacement {
            get {
                if (IsDE)
                    return 0.02;
                return 0.45;
            }
        }
    }
}
