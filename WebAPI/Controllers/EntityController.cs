using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EntityController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllScenarios()
        {
            //MOCK

            List<Dictionary<string, string>> scenarioList = new List<Dictionary<string, string>>();

            Dictionary<string, string> scenario = new Dictionary<string, string>();
            scenario.Add("MainVersion", "0");
            scenario.Add("MinVersion", "0");
            scenario.Add("StateDescription", "READY");
            scenario.Add("Date", "2012-12-31T00:00:00");
            scenario.Add("Type", "SES");
            scenario.Add("Description", "DE022L");
            scenario.Add("TypeEconomy", "EUR");

            scenarioList.Add(scenario);

            scenario = new Dictionary<string, string>();
            scenario.Add("MainVersion", "0");
            scenario.Add("MinVersion", "21");
            scenario.Add("StateDescription", "CALIBRATED");
            scenario.Add("Date", "2013-12-31T00:00:00");
            scenario.Add("Type", "EBS");
            scenario.Add("Description", "BASE");
            scenario.Add("TypeEconomy", "EUR");

            scenarioList.Add(scenario);

            return Request.CreateResponse<List<Dictionary<string, string>>>(HttpStatusCode.OK, scenarioList);
        }

       
        //TODO
        [HttpPost]
        public HttpResponseMessage SaveCurrentScenario()
        {
            return null;
        }
    }
}
