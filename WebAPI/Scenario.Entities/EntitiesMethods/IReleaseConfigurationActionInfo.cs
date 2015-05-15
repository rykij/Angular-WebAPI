using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public interface IReleaseConfigurationActionInfo
    {
        string TestDestinationFile { get; set; }
        IDictionary<SourceFileIdentifier, string> CertainityEquivalentTestDestinationFiles { get; set; }
        IDictionary<SourceFileIdentifier, string> ScenarioDataFiles { get; set; }
        bool IsSpecificCountry { get; set; }
        int CeYears { get; set; }
        int[] AvailableCeYears { get;  }
        int DefaultCeYears { get; }
        string BaseReleaseFolder { get; set; }
        string BaseVaReleaseFolder { get; set; }
        string ReleaseFolder { get; set; }
        string PublishFolder { get; set; }
        bool CompressScenarioDataFiles { get; set; }
        string CompressionFileName { get; set; }
    }

    public class SourceFileIdentifier { 
        public string Main = string.Empty;
        public string Alternative = string.Empty;
    }
}
