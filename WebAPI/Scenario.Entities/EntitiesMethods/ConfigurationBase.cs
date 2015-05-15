using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public class ConfigurationBase
    {

        protected enum Status
        {
            READY,
            ELABORATING,
            CALIBRATING,
            ELABORATED,
            CALIBRATED,
            PUBLISHED,
            RELEASED,
            ARCHIVED
        }

        private enum TimestepType
        {
            ANNUAL,
            MONTHLY
        }

        public static readonly int ScenarioVersionFactor = 100;

        protected int GetMainVersion(int Version)
        {
            return (Version / ScenarioVersionFactor);
        }

        protected int GetMinVersion(int Version)
        {
            return Version % ScenarioVersionFactor;
        }

        protected string GetStateDescription(int State)
        {
            return ((Status)State).ToString();
        }
    }
}
