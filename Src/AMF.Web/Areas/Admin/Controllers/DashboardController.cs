using System.Web.Mvc;
using AMF.Core.Storage;
using AMF.Web.Annotations;

namespace AMF.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class DashboardController : Controller
    {
        private readonly ISession _session;

        public DashboardController(ISession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}