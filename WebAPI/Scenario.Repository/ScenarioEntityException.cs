using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scenario.Repository
{
    public class ScenarioEntityException : Exception
    {
        public ScenarioEntityException(string message, Exception e) : base(message,e) { 
            
        }
        public ScenarioEntityException(string message)
            : base(message)
        {

        }
    }
}
