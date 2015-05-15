using System;
using System.Configuration;
using System.Web;
using WebAPI.Security;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

        }

        //To avoid CORS in pre-request OPTIONS if present in client request
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                string accessControlAddress = ConfigurationManager.AppSettings["AccessControlAddress"];
                AddCustomHeaderFilter headerFilter = new AddCustomHeaderFilter(accessControlAddress, true);
            }
        }
    }
}