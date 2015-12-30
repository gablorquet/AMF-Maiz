using System.Web;
using System.Web.Mvc;
using AMF.Core.Authentication;
using Newtonsoft.Json;

namespace AMF.Web.Annotations
{
    public class AuthorizeAdmin : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            
            var data = HttpContext.Current.User.Identity.Name;
 
       
            var user = JsonConvert.DeserializeObject<CurrentUser>(data);


            if (user != null && user.Type == UserType.Animateur)
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
