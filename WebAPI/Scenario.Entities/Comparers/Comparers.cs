using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{

    public class ConfigurationCurvesComparer : IEqualityComparer<Configuration>
    {

        public bool HasSameNyc(Configuration Scenario, Configuration Scenario1)
        {
            int curveCount = Scenario.NominalRates.Intersect(Scenario1.NominalRates, new NominalRateComparer()).Count();
            if (curveCount < Scenario.NominalRates.Count || curveCount < Scenario1.NominalRates.Count || Scenario.NominalRates.Count != Scenario1.NominalRates.Count)
                return false;
            return true;
        }

        public bool HasSameReal(Configuration Scenario, Configuration Scenario1)
        {
            int curveCount = Scenario.Reals.Intersect(Scenario1.Reals, new RealCurveComparer()).Count();
            if (curveCount < Scenario.Reals.Count || curveCount < Scenario1.Reals.Count || Scenario.Reals.Count != Scenario1.Reals.Count)
                return false;
            return true;
        }

        public bool HasSameSwaptions(Configuration Scenario, Configuration Scenario1)
        { 
            int curveCount = Scenario.Swaptions.Intersect(Scenario1.Swaptions, new SwaptionComparer()).Count();
            if (curveCount < Scenario.Swaptions.Count || curveCount < Scenario1.Swaptions.Count || Scenario.Swaptions.Count != Scenario1.Swaptions.Count)
                return false;
            return true;
        }

        public bool HasSameInflations(Configuration Scenario, Configuration Scenario1)
        {
            int curveCount = Scenario.Inflations.Intersect(Scenario1.Inflations, new InflationComparer()).Count();
            if (curveCount < Scenario.Inflations.Count || curveCount < Scenario1.Inflations.Count || Scenario.Inflations.Count != Scenario1.Inflations.Count)
                return false;
            curveCount = Scenario.InflationsCe.Intersect(Scenario1.InflationsCe, new InflationComparer()).Count();
            if (curveCount < Scenario.InflationsCe.Count || curveCount < Scenario1.InflationsCe.Count || Scenario.InflationsCe.Count != Scenario1.InflationsCe.Count)
                return false;
            return true;
        }

        public bool HasSameEquities(Configuration Scenario, Configuration Scenario1)
        {
            int curveCount = Scenario.Equities.ToList().Intersect(Scenario1.Equities.ToList(), new EquityComparer()).Count();
            if (curveCount < Scenario.Equities.Count || curveCount < Scenario1.Equities.Count || Scenario.Equities.Count != Scenario1.Equities.Count)
                return false;
            return true;
        }

        public bool HasSameCredits(Configuration Scenario, Configuration Scenario1)
        {
            int curveCount = Scenario.Credits.Intersect(Scenario1.Credits, new CreditComparer()).Count();
            if (curveCount < Scenario.Credits.Count || curveCount < Scenario1.Credits.Count || Scenario.Credits.Count != Scenario1.Credits.Count)
                return false;
            return true;
        }

        public bool HasSameProperies(Configuration Scenario, Configuration Scenario1)
        {
            int curveCount = Scenario.Properties.Intersect(Scenario1.Properties, new PropertyComparer()).Count();
            if (curveCount < Scenario.Properties.Count || curveCount < Scenario1.Properties.Count || Scenario.Properties.Count != Scenario1.Properties.Count)
                return false;
            return true;
        }

        public bool Equals(Configuration Scenario, Configuration Scenario1)
        {
            if (HasSameNyc(Scenario, Scenario1) == false)
                return false;
            if(HasSameReal(Scenario,Scenario1) == false)
                return false;
            if(HasSameInflations(Scenario,Scenario1) == false)
                return false;
            if(HasSameEquities(Scenario,Scenario1) == false)
                return false;
            if(HasSameProperies(Scenario,Scenario1) == false)    
                return false;
            if(HasSameSwaptions(Scenario,Scenario1) == false)
                return false;
            if(HasSameCredits(Scenario,Scenario1) == false)
                return false;

            return true;
        }

        public int GetHashCode(Configuration obj)
        {
            return obj.Economy.GetHashCode() ^ obj.ScenarioType.ModelType.GetHashCode();

        }
    }

    public static class DoubleComparer
    {
        public static bool Equals(double x, double y)
        {
            return Math.Round(x, 12).Equals(Math.Round(y, 12));
        }
    }

    public class NominalRateComparer : IEqualityComparer<NominalRate>
    {
        public bool Equals(NominalRate x, NominalRate y)
        {
            return (
                    x.Time == y.Time &&
                    x.Id == y.Id);
        }

        public int GetHashCode(NominalRate obj)
        {
            return
                obj.Time.GetHashCode() ^
                obj.Id;
        }
    }

    public class RealCurveComparer : IEqualityComparer<Real>
    {

        public bool Equals(Real x, Real y)
        {
            return DoubleComparer.Equals(x.Time, y.Time) && DoubleComparer.Equals(x.Value, y.Value);
        }

        public int GetHashCode(Real x)
        {
            return x.Time.GetHashCode() ^ x.Value.GetHashCode();
        }
    }

    public class PropertyComparer : IEqualityComparer<Property>
    {
        public bool Equals(Property x, Property y)
        {
            return x.Id == y.Id && x.Idx == y.Idx && x.Type == y.Type;
        }

        public int GetHashCode(Property x)
        {
            return x.Id.GetHashCode() ^ x.Idx.GetHashCode() ^ x.Type.GetHashCode();
        }
    }

    public class SwaptionComparer : IEqualityComparer<Swaption>
    {
        public bool Equals(Swaption x, Swaption y)
        {
            return x.ID == y.ID && x.Time == y.Time && x.Maturity == y.Maturity;
        }

        public int GetHashCode(Swaption x)
        {
            return x.ID.GetHashCode() ^ x.Time.GetHashCode() ^ x.Maturity.GetHashCode();
        }
    }

    public class CreditComparer : IEqualityComparer<Credit>
    {
        public bool Equals(Credit x, Credit y)
        {
            return x.DefaultCredit.Maturity == y.DefaultCredit.Maturity && x.DefaultCredit.Rating == y.DefaultCredit.Rating && x.SpreadAdj == y.SpreadAdj;
        }

        public int GetHashCode(Credit x)
        {
            return x.SpreadAdj.GetHashCode() ^ x.DefaultCredit.GetHashCode() ^ x.DefaultCredit.GetHashCode();
        }
    }

    public class EquityComparer : IEqualityComparer<Equity>
    {
        public bool Equals(Equity x, Equity y)
        {
            return (x.Date == y.Date &&
                    x.Economy == y.Economy &&
                    x.Maturity == y.Maturity &&
                    x.Idx == y.Idx &&
                    x.Type == y.Type &&
                    x.ID == y.ID);
        }

        public int GetHashCode(Equity obj)
        {
            return obj.Date.ToLongTimeString().GetHashCode() ^
                obj.Economy.GetHashCode() ^
                obj.Maturity.GetHashCode() ^
                obj.Idx.GetHashCode() ^
                obj.Type.GetHashCode() ^
                obj.ID;
        }
    }

    public class InflationComparer : IEqualityComparer<Inflation>
    {
        public bool Equals(Inflation x, Inflation y)
        {
            return (
                    x.Time == y.Time &&
                    x.Id == y.Id
                    );
        }

        public int GetHashCode(Inflation obj)
        {
            return
                obj.Time.GetHashCode() ^
                obj.Id;

        }
    }

    public class FactorComparer : IEqualityComparer<Factor>
    {
        public bool Equals(Factor x, Factor y)
        {
            return (
                    x.B1.Equals(y.B1) &&
                    x.B2.Equals(y.B2) &&
                    x.Index == y.Index
                    );
        }

        public int GetHashCode(Factor obj)
        {
            return
                 obj.B1.GetHashCode() ^
                 obj.B2.GetHashCode() ^
                 obj.Index;
        }
    }
}
