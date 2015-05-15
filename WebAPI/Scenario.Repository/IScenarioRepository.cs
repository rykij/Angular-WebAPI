using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scenario.Entities;

namespace Scenario.Repository
{
    public interface IScenarioRepository
    {
        void Save(Configuration Scenario);
        void SaveCreditFor(Configuration Scenario);
        void Delete(Configuration Scenario);
        void DeleteCredits(Configuration Scenario);
        IList<ScenarioSummary> GetAll(bool OnlyMainEconomy=true);
        IList<Configuration> GetScenariosOfType(ScenarioType Type);
        Configuration Load(int ScenarioId);
        Configuration Get(int ScenarioId);
        Configuration GetBaseFor(Configuration Scenario, bool SameDate=true);
        IList<Equity> GetEquitiesFor(Configuration Scenario);
        SwaptionCurve GetSwaptionsFor(Configuration Scenario);
        SwaptionCurve GetMarketSwaptionsFor(Configuration Scenario);
        InflationCurve  GetInflationsFor(Configuration Scenario);
        InflationCurve GetCertaintyEquivalentInflationsFor(Configuration Scenario);
        IList<Property> GetPropertiesFor(Configuration Scenario);
        NominalCurve GetNominalRatesFor(Configuration config, string LiquidityLevel="");
        IList<Credit> GetCreditsFor(Configuration Scenario);
        IList<ScenarioType> GetScenarioTypes(int year);
        IList<BhcTemplate> GetTemplatesFor(Configuration Scenario);
        Configuration Get(ScenarioType type);
        IList<IResult> GetResults(Configuration Scenario, params int[] Trials);
        void ClearResultsFor(Configuration Scenario, int UpToTrial = 0);
        int GetNextVersionFor(Configuration Scenario);
        int GetNextSeedVersionFor(Configuration Scenario);
        IList<Model> GetModels();
        ModelsFactor GetFactorsFor(Configuration Scenario);
        TestWeight GetTestWeightsFor(Configuration Scenario);
    }
}
