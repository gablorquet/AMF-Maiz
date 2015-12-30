using System.Linq;
using System.Web.Mvc;
using AMF.Core.Authentication;
using AMF.Core.Extensions;
using AMF.Core.Model;
using AMF.Core.Storage;
using AMF.Web.Areas.Admin.ViewModels;

namespace AMF.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authentication;
        private readonly ISession _session;

        public LoginController(IAuthenticationService authentication,
            ISession session)
        {
            _authentication = authentication;
            _session = session;
        }

        public ActionResult Index()
        {
            return _authentication.GetCurrent<User>() != null
                ? WhenLoggedIn() :
                View("Login", new LoginViewModel());
        }

        public ActionResult Login()
        {
            return _authentication.GetCurrent<User>() != null
                ? WhenLoggedIn() :
                View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (_authentication.GetCurrent<User>() != null)
            {
                return WhenLoggedIn();
            }

            var encryptedPassword = model.Password.ToSHA1();

            var user = _session.Set<User>()
                .Where(x => x.Username == model.Username)
                .Where(x => x.Password == encryptedPassword)
                .FirstOrDefault(x => !x.Archived.HasValue);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Login info");
                return View(model);
            }

            _authentication.SetCurrent(user);


            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return WhenLoggedIn();

        }


        private ActionResult WhenLoggedIn()
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

    }

}