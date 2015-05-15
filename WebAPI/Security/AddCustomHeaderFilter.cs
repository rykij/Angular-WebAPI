using System.Web;
using System.Web.Http.Filters;

namespace WebAPI.Security
{
    //Add header property on response for enable CORS (Cross Origin Resource Sharing) on browser
    public class AddCustomHeaderFilter : ActionFilterAttribute
    {
        private string AccessControlAddress { get; set; }

        public AddCustomHeaderFilter(string accessControlAddress, bool isBeginRequest)
        {
            this.AccessControlAddress = accessControlAddress;
            if (isBeginRequest) SetHeaderFilter();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Content.Headers.Add("Access-Control-Allow-Origin", this.AccessControlAddress);
            actionExecutedContext.Response.Content.Headers.Add("Access-Control-Allow-Methods", "GET");
            actionExecutedContext.Response.Content.Headers.Add("Access-Control-Allow-Methods", "POST");
            base.OnActionExecuted(actionExecutedContext);
        }

        public void SetHeaderFilter()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", this.AccessControlAddress);
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }
    }
}