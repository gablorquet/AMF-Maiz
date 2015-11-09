using System.Web.Mvc;
using AMF.Core.Storage;

namespace AMF.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;

        public HomeController(ISession session)
        {
            _session = session;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}