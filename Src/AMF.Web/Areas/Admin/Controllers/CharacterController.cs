using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using AMF.Core.Model;
using AMF.Core.Storage;
using AMF.Web.Annotations;
using AMF.Web.Areas.Admin.ViewModels;
using RequireJsNet;

namespace AMF.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class CharacterController : Controller
    {
        private readonly ISession _session;

        public CharacterController(ISession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            var currentEvent = _session.Set<Event>()
                .First(x => x.NextEvent);

            var chars = _session.Set<Character>()
                .Where(x => x.Year.Current)
                .ToList();

            var models = chars.Select(x => new CharacterViewModel(x, currentEvent)).ToList();

            return View(models);
        }

        public ActionResult Create(int playerId)
        {
            var currentEvent = _session.Set<Event>()
                .First(x => x.NextEvent);

            var player = _session.SingleById<Player>(playerId);

            if (player == null)
                return new HttpNotFoundResult();

            if (currentEvent == null)
                return new HttpNotFoundResult();

            if (currentEvent.Attendees.Any(x => x.Player.Id == playerId))
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            var model = new CharacterViewModel(player, currentEvent);

            RequireJsOptions.Add("model", model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CharacterViewModel data)
        {
            if (!ModelState.IsValid)
                return View(data);

            var character = data.AsCharacter();

            _session.Add(character);

            return View();
        }


        public ActionResult Update(int characterId)
        {
            var currentEvent = _session.Set<Event>().First(x => x.NextEvent);

            var character = _session.SingleById<Character>(characterId);

            if (character == null)
                return new HttpNotFoundResult();

            var model = new CharacterViewModel(character, currentEvent);

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(CharacterViewModel data)
        {
            if (!ModelState.IsValid)
                return View(data);

            var character = data.AsCharacter();

            var original = _session.SingleById<Character>(character.Id);

            original.UpdateFrom(character);

            _session.Commit();

            return RedirectToAction("Index", "Dashboard");
        }

        public JsonResult GetSkillAvailableForCharacter(int characterId, int[] selectedCategories, int[] selectedSkills)
        {
            var character = _session.SingleById<Character>(characterId);

            if (character == null)
                return Json("", JsonRequestBehavior.AllowGet);

            var skills = _session.Set<Skill>()
                .Where(x => character.Categories.Select(y => y.Id).Contains(x.Category.Id) ||
                            selectedCategories.Contains(x.Category.Id))
                .Where(x => x.Prerequisites
                    .Any(y => 
                        !y.Prerequisites.Any() ||
                        character.Skills.Select(z => z.Id).Contains(y.Id) ||
                        x.Prerequisites.Any(a => selectedSkills.Contains(a.Id))))
                .ToList();

            return Json(skills.Select(x => new SkillViewModel(x)), JsonRequestBehavior.AllowGet);
        }
    }
}