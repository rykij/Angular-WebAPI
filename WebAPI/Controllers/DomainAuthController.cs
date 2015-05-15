using System;
using System.DirectoryServices.AccountManagement;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class DomainAuthController : ApiController
    {
        public DomainAuthController() { }

        [HttpPost]
        public HttpResponseMessage IsDomainUser([FromBody]UserModel user)
        {
            if (!String.IsNullOrEmpty(user.UserName) && !String.IsNullOrEmpty(user.Password))
            {
                if (CheckUserDomain(user))
                {
                   
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(
                            "Authentication success"
                        )
                    };
                }
                else
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(
                            "Authentication failed"
                        )
                    };
                }
            }
            else
            {
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Please verify the credentials.")};
            }
        }

        public bool CheckUserDomain(UserModel user)
        {
            bool valid = false;
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                valid = context.ValidateCredentials(user.UserName, user.Password);
            }
            return valid;
        }       
    }
}
