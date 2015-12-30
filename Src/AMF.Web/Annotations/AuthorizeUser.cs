using System.Web;
using System.Web.Mvc;

namespace AMF.Web.Annotations
{
    public class AuthorizeUser : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
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