using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Scenario.Entities;

namespace Scenario.Repository
{
    public static class RepoHelper
    {
        public static string GetTypeMonthlyAnnual(string BaseType, bool IsMonthly)
        {
            return BaseType + (IsMonthly ? "_M" : "");
        }

        public static string GetTypeTemplate(bool IsSwap)
        {
            string templType = "Gov";
            if (IsSwap)
            {
                templType = "Swap";
            }

            return templType;
        }

        public static DateTime GetMaxDate<T>(DbSet<T> ScenarioCurves, Entities.ITypedScenarioCurve SampleCurve) where T : class, Entities.ITypedScenarioCurve
        {
            var curves = ScenarioCurves.Where(
                    a => a.Date <= SampleCurve.Date
                     && a.Type.Equals(SampleCurve.Type) && a.Economy.Equals(SampleCurve.Economy)
                    );
            DateTime maxDate = curves.Max(d => d.Date);

            return maxDate;
        }

        public static IList<T> GetCurvesFor<T>(DbSet<T> ScenarioCurves, string Type, Entities.Configuration Scenario) where T : class, Entities.ITypedScenarioCurve
        {
            IList<T> curves = null;
            var samplecurve = new TypedCurve() { Date = Scenario.ScenarioType.ReferenceScenarioDate, Economy = GetScenarioEconomy(Scenario), Type = Type };
            try
            {
                curves = GetCurvesFor<T>(ScenarioCurves, samplecurve);
            }
            catch (Exception)
            {
                samplecurve.Economy = Scenario.Economy;
                curves = GetCurvesFor<T>(ScenarioCurves, samplecurve);
            }
            return curves;
        }

        public static IList<T> GetCurvesFor<T>(DbSet<T> ScenarioCurves, Entities.ITypedScenarioCurve SampleCurve) where T : class, Entities.ITypedScenarioCurve
        {
            DateTime maxDate = GetMaxDate<T>(ScenarioCurves, SampleCurve);

            return ScenarioCurves.Where<T>(
                eq => eq.Date.Equals(maxDate)
                    && eq.Type.Equals(SampleCurve.Type)
                    && eq.Economy.Equals(SampleCurve.Economy)).ToList<T>();
        }

        public static NominalCurve GetNominalRatesFor(DbSet<NominalCurve> NominalCurves, ExtendedTypedCurve SampleCurve)
        {
           int maxGid =
                NominalCurves.Where(
                eq => eq.Date == SampleCurve.Date
                    && eq.Type == SampleCurve.Type
                    && eq.Economy == SampleCurve.Economy
                    && eq.LiquidityLevel == SampleCurve.LiquidityLevel
                    ).Max(nn=>nn.GID);

            var curve = from n in NominalCurves
                        where n.GID == maxGid
                        select n;
            
            return curve.First();
        }

        public static NominalCurve GetNominalRatesFor(DbSet<NominalCurve> NominalCurves, string LiquidityLevel, Configuration Scenario)
        {
            string liquidityLevel = LiquidityLevel.Equals(string.Empty) ? Scenario.ScenarioType.LiquidityLevel : LiquidityLevel;
            string type = RepoHelper.GetTypeMonthlyAnnual(Scenario.ScenarioType.TypeForNominal, Scenario.ModelParameter.Models.IsMonthly);
            double nominalMaxTime = Scenario.ModelParameter.Models.TimeStepMultiply * Scenario.ModelParameter.Models.ModelledYears;
            NominalCurve curves = null;

            ExtendedTypedCurve sample = new ExtendedTypedCurve()
            {
                Date = Scenario.ScenarioType.ReferenceScenarioDate,
                Type = type,
                LiquidityLevel = liquidityLevel,
                Time = nominalMaxTime,
                Economy = GetScenarioEconomy(Scenario),
            };
            try
            {
                curves = GetNominalRatesFor(NominalCurves, sample);
            }
            catch (Exception)
            {
                sample.Economy = Scenario.Economy;
                curves = GetNominalRatesFor(NominalCurves, sample);
            }

            if (curves.NominalRates.Count() != Scenario.ModelParameter.Models.TimeStepMultiply * Scenario.ModelParameter.Models.ModelledYears)
                throw new Exception("Nominal Rates with wrong time extension");

            return curves;
        }

        public static IList<DefaultCredit> GetDefaultCreditsFor(string Econmony, IList<DefaultCredit> DefaultCredits, Configuration Scenario)
        {
            var defaultCredits = DefaultCredits.
                    Where(
                        a => a.Date.Equals(Scenario.ScenarioType.ReferenceScenarioDate)
                        && a.Economy.Equals(Econmony) && a.Type.Equals(Scenario.ScenarioType.TypeForDefaultCredit)
                    ).ToList();
            if (defaultCredits.Count == 0)
            {
                defaultCredits = DefaultCredits.
                    Where(
                        a => a.Date.Equals(Scenario.ScenarioType.ReferenceScenarioDate)
                        && a.Economy.Equals(Econmony) && a.Type.Equals(Scenario.ScenarioType.BaseTypeForDefaultCredit)
                    ).ToList();

            }
            if (defaultCredits.Count == 0 && Scenario.ScenarioType.BaseTypeForDefaultCredit.Contains('_'))
            {
                defaultCredits = DefaultCredits.
                    Where(
                        a => a.Date.Equals(Scenario.ScenarioType.ReferenceScenarioDate)
                        && a.Economy.Equals(Econmony) && a.Type.Contains(Scenario.ScenarioType.BaseTypeForDefaultCredit.Split('_')[1])
                    ).ToList();

            }
            if (defaultCredits.Count == 0)
            {
                defaultCredits = DefaultCredits.
                    Where(
                        a => a.Date.Equals(Scenario.ScenarioType.ReferenceScenarioDate)
                        && a.Economy.Equals(Econmony) && a.Type.Contains(Scenario.ScenarioType.BaseTypeForDefaultCredit)
                    ).ToList();

            }

            return defaultCredits;
        }

        public static IList<DefaultCredit> GetDefaultCredits(IList<DefaultCredit> DefaultCredits, Configuration Scenario)
        {
            string economy = GetScenarioEconomy(Scenario);
            var defaultCredits = GetDefaultCreditsFor(economy,DefaultCredits,Scenario);
            if (defaultCredits.Count == 0)
            {
                economy = Scenario.Economy;
                defaultCredits = GetDefaultCreditsFor(economy, DefaultCredits, Scenario);
            }
            if(defaultCredits.Count == 0)
                throw new Exception("unable to Load Default Credits for " + Scenario.Identifyer); 
            int version = defaultCredits.
               Where(
                   a => a.Date.Equals(Scenario.ScenarioType.ReferenceScenarioDate)
                   && a.Economy.Equals(economy)
               ).Max(d => d.Version);

            defaultCredits = defaultCredits.Where(
                cr => cr.Economy.Equals(economy)
                    && cr.Date.Equals(Scenario.ScenarioType.ReferenceScenarioDate)
                    && cr.Version.Equals(version)).ToList();

            return defaultCredits;
        }

        public static string GetScenarioEconomy(Entities.Configuration Scenario)
        {
            if (Scenario.Parent == null)
                return Scenario.Economy + "_" + Scenario.ScenarioType.Country;
            else
                return Scenario.Economy + "_" + Scenario.Parent.Economy;
        }
    }
}
