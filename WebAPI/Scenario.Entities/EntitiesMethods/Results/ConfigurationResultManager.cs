using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public class ConfigurationResultManager
    {

        static readonly string[] ResultTables = new string[] { 
            "Scenario.Entities.CURVES_MC_1",
            "Scenario.Entities.CURVES_MC_2", 
            "Scenario.Entities.EXTRA", 
            "Scenario.Entities.SUMMARY", 
            "Scenario.Entities.REALC", 
            "Scenario.Entities.TRANSITION_MATRIX" };
        
        public ConfigurationResultManager() {
        }

        public IList<IResult> CreateAndFillResults(IList<ConfigurationResult> Results)
        {
            List<IResult> results = new List<IResult>();

            foreach (ConfigurationResult result in Results)
            {
                IList<IResult> localResult = CreateAndFillResult(result);
                if (localResult.Count == 0)
                    throw new Exception("unknown property " + result.ColumnName);
                MergeResults(results, localResult);
            }

            return results;
        }

        private void MergeResults(List<IResult> Results, IList<IResult> MergingResult)
        {
            
            foreach (IResult mergeResult in MergingResult) {
                bool merged = false; 
                foreach (IResult result in Results.Where(
                    r=>r.GetType().Equals(mergeResult.GetType())
                    && r.Timestep.Equals(mergeResult.Timestep)
                    && r.Trial.Equals(mergeResult.Trial)))
                {
                    foreach (var prop in mergeResult.GetType().GetProperties())
                    {
                        double? value = prop.GetValue(mergeResult, null) as double?;
                        if (null != value &&
                            (value != 0 || prop.PropertyType.FullName.Contains("Nullable"))
                            )
                        {
                            prop.SetValue(result, value, null);
                        }
                    }
                    merged = true;
                }
                if (merged == false) {
                    Results.Add(mergeResult);
                }
            }
        }


        public IList<IResult> CreateAndFillResult(ConfigurationResult Result)
        {
            IList<IResult> results = new List<IResult>();

            for (int timestep = 0; timestep < Result.Values.Count; timestep++)
            {
                foreach (string table in ResultTables)
                {
                    Type tableType = Type.GetType(table);
                    IResult tableInstance = (IResult)Activator.CreateInstance(tableType);

                    var props= tableType.GetProperties().Where(p=>p.Name.ToUpper().Equals(Result.ColumnName.ToUpper()));
                    var prop = (props.Count() > 0 ? props.First() : null);
                    if (prop != null)
                    {
                        prop.SetValue(tableInstance, Result.Values[timestep], null);
                        tableInstance.Timestep = timestep;
                        tableInstance.Trial = Result.Trial;
                        results.Add(tableInstance);
                        break;
                    }
                }
            }

            return results;
        }

        public ConfigurationResult FillConfigurationResultValues(ConfigurationResult ConfigurationResult,IList<IResult> Results)
        {
            ConfigurationResult.Values = new List<double>();
            foreach (IResult result in Results.Where(r=>r.Trial.Equals(ConfigurationResult.Trial)).OrderBy(r=>r.Timestep))
            {
                Type resultType = result.GetType();
                var props = resultType.GetProperties().Where(p => p.Name.ToUpper().Equals(ConfigurationResult.ColumnName.ToUpper()));
                var prop = (props.Count() > 0 ? props.First() : null);
                if (prop != null)
                {
                    double? val = prop.GetValue(result, null) as double?;
                    if (val != null)
                    {
                        ConfigurationResult.Values.Add((double)val);
                    }
                }
            }
            return ConfigurationResult;
        }
    }
}
