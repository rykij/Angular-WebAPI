using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class ScenarioSummary : ConfigurationBase
    {
        public int MainVersion
        {
            get { return GetMainVersion(Version); }
        }

        public int MinVersion
        {
            get { return GetMinVersion(Version); }
        }

        public static ScenarioSummary GetSummaryFrom(Configuration Scenario)
        {
            ScenarioSummary summ = new ScenarioSummary()
            {
                Country = Scenario.Country,
                Economy = Scenario.Economy,
                Version = Scenario.Version,
                ID = Scenario.ID,
                Status = Scenario.status
            };
            if (Scenario.ModelParameter != null) {
                summ.TypeDescription = Scenario.ModelParameter.Models.TypeDescription;
            }
            if(Scenario.ScenarioType != null){
                summ.TypeEconomy = Scenario.ScenarioType.Economy;
                summ.Date = Scenario.ScenarioType.ScenarioDate;
                summ.Description = Scenario.ScenarioType.Description;
                summ.Type = Scenario.ScenarioType.ModelType;
            }
            if (Scenario.Parent != null)
                summ.ParentId = Scenario.Parent.ID;
                
            return summ;
        }

        public string StateDescription {
            get { return GetStateDescription(this.Status); }
        }
    }
}
