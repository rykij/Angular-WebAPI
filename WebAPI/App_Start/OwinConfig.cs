using System.Web.Http;
using Microsoft.Owin;
using Owin;

//Main start-up class
/*Install-Package Microsoft.AspNet.Identity.Owin and 
  Microsoft.Owin.Security.OAuth from Nuget */
[assembly: OwinStartup(typeof(WebAPI.App_Start.OwinConfig))]
namespace WebAPI.App_Start
{
    public class OwinConfig
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}