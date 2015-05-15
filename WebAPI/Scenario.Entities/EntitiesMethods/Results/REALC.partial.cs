using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Entities
{
    public partial class REALC : IResult, IIResult
    {
        public double GetReal(int Years)
        {
            double finalvalue = 0;
            Type resultType = this.GetType();
            var props = resultType.GetProperties().Where(p => p.Name.Contains("Real__" + Years + "y"));
            var prop = (props.Count() > 0 ? props.First() : null);
            if (prop != null)
            {
                double? val = prop.GetValue(this, null) as double?;
                if (val != null)
                {
                    finalvalue = (double)val;
                }
            }

            return finalvalue;
        }

        public double[] Values {
            get
            {
                List<double> res = new List<double>();
                for (int year = 1; year <= 60; year++)
                {
                    res.Add(GetReal(year));
                }

                return res.ToArray();
            }
        }
    }
}
