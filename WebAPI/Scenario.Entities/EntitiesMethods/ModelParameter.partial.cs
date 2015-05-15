using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class ModelParameter
    {
        public static readonly int ExchangeRateDefault = 1;
        
        public enum CalibrationType
        {
            SWAPTIONS = 1,
            EQUITY = 2,
            REAL = 4,
            CREDIT = 8,
            CERTEQUIVALENT = 16
        }

        public enum BoundingMethod { 
            CAPFloor,
            Freeze,
            CapFloorFreeze
        }

        public static IDictionary<BoundingMethod, string> Bounding = new Dictionary<BoundingMethod, string>() { 
            {BoundingMethod.CAPFloor,"Cap/Floor"},
            {BoundingMethod.Freeze,"Freeze"},
            {BoundingMethod.CapFloorFreeze,"Cap/Floor and Freeze"},
        };

        public ModelParameter Clone
        {
            get
            {
                ModelParameter clone = new ModelParameter();
                CopyAllValues(clone);

                return clone;
            }
        }

        public void CopyValues(ModelParameter To)
        {
            To.r0 = this.r0;
            To.a = this.a;
            To.g = this.g;
            To.m = this.m;
            To.m0 = this.m0;
            To.na1 = this.na1;
            To.na2 = this.na2;
            To.ns1 = this.ns1;
            To.ns2 = this.ns2;
            To.ra1 = this.ra1;
            To.ra2 = this.ra2;
            To.rs1 = this.rs1;
            To.rs2 = this.rs2;
            To.s0 = this.s0;
            To.sinf = this.sinf;
            To.sesReal1 = this.sesReal1;
            To.sesReal2 = this.sesReal2;
            To.LMMPlus_a = this.LMMPlus_a;
            To.LMMPlus_b = this.LMMPlus_b;
            To.LMMPlus_c = this.LMMPlus_c;
            To.LMMPlus_d = this.LMMPlus_d;
            To.LMMPlusRevLevel = this.LMMPlusRevLevel;
            To.LMMPlusRevRate = this.LMMPlusRevRate;
            To.LMMPlusVol = this.LMMPlusVol;
            To.LMMPlusVolCorr = this.LMMPlusVolCorr;
            To.LMMPlusVolInit = this.LMMPlusVolInit;
            To.SVJDCOrr = this.SVJDCOrr;
            To.SVJDJumpArrivalRate = this.SVJDJumpArrivalRate;
            To.SVJDJumpMean = this.SVJDJumpMean;
            To.SVJDJumpVol = this.SVJDJumpVol;
            To.SVJDRevLevel = this.SVJDRevLevel;
            To.SVJDRevSpeed = this.SVJDRevSpeed;
            To.SVJDVol = this.SVJDVol;
            To.SVJDVolInit = this.SVJDVolInit;
            To.ExchangeRateVol = this.ExchangeRateVol;
            To.ExchangeRateStart = this.ExchangeRateStart;
            To.ExchangeRateMu = this.ExchangeRateMu;
            To.ModelsFactor = this.ModelsFactor;
            To.SwaptionCurve = this.SwaptionCurve;
            To.ExecutedCalibrations = this.ExecutedCalibrations;
            To.LMMPlusRateDispl = this.LMMPlusRateDispl;
            To.nCap = this.nCap;
            To.nFloor = this.nFloor;
            To.nFreezeLower = this.nFreezeLower;
            To.nFreezeUpper = this.nFreezeUpper;
            To.nBounding = this.nBounding;
        }

        public void CopyAllValues(ModelParameter To)
        {
            CopyValues(To);
            
            To.Models = this.Models;
        }


        public ICollection<Factor> Factors
        {
            get
            {
                if (ModelsFactor != null)
                    return ModelsFactor.Factors.ToList().AsReadOnly();
                return new List<Factor>().AsReadOnly();
            }

            set
            {
                var intersection = Factors.ToList().Intersect(value, new FactorComparer());
                bool update = intersection.Count() != value.Count;
                if (update)
                {
                    ModelsFactor = null;
                    var model = new ModelsFactor() { Factors = value , Date = Configuration.ScenarioType.ScenarioDate};
                    model.ModelParameters.Add(this);
                    ModelsFactor = model;
                }
            }
        }

        public ICollection<Swaption> Swaptions
        {
            get
            {
                if (SwaptionCurve != null)
                    return SwaptionCurve.Swaptions.ToList().AsReadOnly();
                return new List<Swaption>().AsReadOnly();
            }

            set
            {
                SwaptionCurve = null;
                var swapcurve = new SwaptionCurve() {Type = "CALIB", Economy = Configuration.Economy, Date = Configuration.ScenarioType.ScenarioDate };
                swapcurve.Swaptions = value;
                SwaptionCurve = swapcurve;
            }
        }

        public bool IsCalibrated(CalibrationType Type) {
            return (ExecutedCalibrations & (int)Type) != 0;
        }

        public void UnsetCalibration(CalibrationType Type)
        {
            ExecutedCalibrations &= ~(int)Type;
        }
        public void SetCalibration(CalibrationType Type)
        {
            ExecutedCalibrations |= (int)Type;
        }
        public void SetDefaultCalibration(CalibrationType Type)
        {
            ExecutedCalibrations |= (int)Type*65536;
        }
        public void UnsetDefaultCalibration(CalibrationType Type)
        {
            ExecutedCalibrations  &= ~ ((int)Type * 65536);
        }
        public bool IsDefaultCalibration(CalibrationType Type)
        {
            return (ExecutedCalibrations & (int)Type * 65536) != 0;
        }
    }
}
