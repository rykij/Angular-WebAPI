using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Hpc;

namespace WebAPI.Controllers
{
    public class JobController : ApiController
    {
        public JobController() { }

        [HttpGet]
        public HttpResponseMessage GetJobStatus()
        {
            //MOCK

            return Request.CreateResponse<Job>(HttpStatusCode.OK, new Job { ID = 1234, Name = "Job Test", Status = 100, Group = "Group31" });
        }

        [HttpPost]
        public HttpResponseMessage RunJob([FromBody]int scenarioId)
        {
            GridCaller.RunJob(scenarioId);
            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    "Job is running!"
                )
            };
        }

        [HttpPost]
        public HttpResponseMessage AbortJob([FromBody]int scenarioId)
        {
            GridCaller.AbortJob(scenarioId);
            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    "Job is aborted!"
                )
            };
        }
    }
}
