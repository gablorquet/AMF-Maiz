using System.Linq;
using System.Web.Mvc;
using AMF.Core.Model;
using AMF.Core.Storage;
using AMF.Web.Areas.Admin.ViewModels;
using RequireJsNet;

namespace AMF.Web.Areas.Admin.Controllers
{
    public class InstitutionController : Controller
    {
        private readonly ISession _session;

        public InstitutionController(ISession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            var year = _session.Set<Year>().First(x => x.Current);
            var institutions = _session.Set<Institution>()
                .Where(x => x.Year == year)
                .ToList();

            var model = new
            {
                institutions = institutions.Select(x => new
                {
                    id = x.Id,
                    nature = x.Nature.Name,
                    leader = x.Leader.Name,
                    members = x.Characters.Count
                })
            };

            RequireJsOptions.Add("model", model);

            return View();
        }

        public ActionResult Create()
        {
            var year = _session.Set<Year>().First(x => x.Current);
            var characters = _session.Set<Character>()
                .Where(x => x.Year == year)
                .Where(x => x.Institution == null)
                .ToList();

            var model = new
            {
                characters = characters.Select(x => new
                {
                    id = x.Id,
                    name = x.Name
                })
            };

            RequireJsOptions.Add("model", model);

            return View();
        }

        [HttpPost]
        public ActionResult Create(InstitutionViewModel data)
        {
            var institution = new Institution
            {
                Name = data.Name,
                Nature = _session.SingleById<Nature>(data.NatureId),
                Leader = _session.SingleById<Character>(data.LeaderId),
                Characters = _session.Set<Character>().Where(x => data.MembersId.Contains(x.Id)).ToList()
            };

            _session.Add(institution);
            _session.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Update(int institutionId)
        {
            var year = _session.Set<Year>().First(x => x.Current);
            var characters = _session.Set<Character>()
                .Where(x => x.Year == year)
                .ToList();

            var institution = _session.SingleById<Institution>(institutionId);

            var model = new
            {
                characters = characters.Select(x => new
                {
                    id = x.Id,
                    name = x.Name
                }),
                institution = new
                {
                    id = institution.Id,
                    name = institution.Name,
                    leader = institution.Leader.Name,
                    members = institution.Characters.Select(y => new
                    {
                        id = y.Id,
                        name = y.Name
                    }).ToList()
                }
            };

            RequireJsOptions.Add("model", model);

            return View();
        }
    }
}