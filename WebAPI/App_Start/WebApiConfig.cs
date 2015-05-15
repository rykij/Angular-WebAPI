using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Http;
using WebAPI.Security;


namespace WebAPI.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           config.MapHttpAttributeRoutes();

           //Just exclude the users controllers from need to provide valid token, so they could authenticate
           config.Routes.MapHttpRoute
           (
           name: "TestAuthApi",
           routeTemplate: "api/{Controller}/{action}",
           defaults: new { controller = "WinAuth", action = "Index" }
           );

           config.Routes.MapHttpRoute
           (
           name: "JobStatusApi",
           routeTemplate: "api/{Controller}/{action}",
           defaults: new { controller = "Job", action = "Index" }
           );

           config.Routes.MapHttpRoute
          (
          name: "ScenarioListApi",
          routeTemplate: "api/{Controller}/{action}",
          defaults: new { controller = "Entity", action = "Index" }
          );

           config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
           );

           //To avoid AJAX call from different domain (CORS)
           string accessControlAddress = ConfigurationManager.AppSettings["AccessControlAddress"];
           config.Filters.Add(new AddCustomHeaderFilter(accessControlAddress, false));

           //config.MessageHandlers.Add(new HTTPSChecker());

           /* Get json on most queries, but you can get xml when you send text/xml */
           config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
           
           config.EnableSystemDiagnosticsTracing();
        }
    }
}
