using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public class ConfigurationResult
    {
        string columnName = null;
        public string ColumnName { get { return GetColumn(); } }
        public IList<double> Values { get; set; }
        public int Trial;
        string field;
        string economy;

        readonly IDictionary<string, string> nameMap = new Dictionary<string, string>() {
            { "NominalZCBP(", "ZC_P_" }, 
            {"RealZCBP(Govt","ZC_P_Real"},
            { "_ 3)", "y" }, 
            { "NominalYieldCurves.NominalYieldCurve.ShortRate", "NShortRate" },
            {"RealYieldCurve.ShortRate","RShortRate"},
            {"ExchangeRate.NominalValue","ExchangeRateN"},
            {"ExchangeRate.RealValue","ExchangeRateR"},
            {"NominalYieldCurves.NominalYieldCurve.CashTotalReturn","Cash_TotRet"},
            { "CreditModels.CreditModel.CreditStochasticDriver","CreditRP" },
            { "PortfolioProportion(","_to_" }
        };

        readonly IList<string> removeList = new List<string>() { 
            "ESG.Economies.", 
            "ESG.Assets.EquityAssets.E_",
            "ESG.Assets.EquityAssets", 
            "ESG.Assets.FixedIncome.GenericBondPortfolios",
            "InflationRates",
            ".", 
            "Value",
            ")",
            "(",
            "PP"
        };
        
        public ConfigurationResult(string Economy,string FieldName) {
            economy = Economy;
            field = FieldName;
            
            nameMap.Add("E_" + economy + ".TotalReturn", "Equity_TotRet");
            nameMap.Add("E_" + economy + ".DividendYield", "Equity_DivYield");
            nameMap.Add("DecorrelatedZScore(ESG.Economies." + economy + ".NominalYieldCurves.NominalYieldCurve)", "DecorrelatedZScore");
            nameMap.Add("DecorrelatedZScore(ESG.Economies." + economy + ".RealYieldCurve)", "DecorrelatedZScore");

            for (int i = 0; i < 9; i++)
            {
                string prop = (i == 0 ? "" : i.ToString());
                nameMap.Add("P_" + economy + prop + ".TotalReturn", "Property" + prop + "_TotRet");
                nameMap.Add("P_" + economy + prop + ".IncomeReturn", "Property" + prop + "_IncomeRet");
                nameMap.Add("P_" + economy + prop + ".CapitalChange", "P" + prop + "CapitalChange");
            }
        }

        private string GetColumn()
        {
            if (columnName == null)
            {
                columnName = field;   
                foreach (string key in nameMap.Keys)
                {
                    columnName = columnName.Replace(key, nameMap[key]);
                }
                foreach (string key in removeList)
                {
                    columnName = columnName.Replace(key, "");
                }

                columnName = columnName.Replace(economy + "_", "");
                columnName = columnName.Replace(economy, "");

                if (columnName.Substring(columnName.Length - 1).Equals("y")) {
                    int subIndex = columnName.LastIndexOf('_');
                    columnName = columnName.Insert(subIndex, "_");
                }
            }

            return columnName;
        }

    }
}
