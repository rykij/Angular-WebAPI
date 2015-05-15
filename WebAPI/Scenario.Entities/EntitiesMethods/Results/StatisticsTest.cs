using System.Collections.Generic;

namespace Scenario.Entities
{

    public class StatisticTest<T>
    {
        public string Type;
        public IList<T> Data;
    }    
}
