using System;
using System.Linq;
using System.Web.Mvc;
using AMF.Core.Model;
using AMF.Core.Storage;
using AMF.Web.Areas.Admin.ViewModels;

namespace AMF.Web.Areas.Admin.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ISession _session;

        public PlayerController(ISession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            var players = _session.Set<Player>().ToList();

            var model = players
                .Where(x => !x.Archived.HasValue)
                .Select(x => new PlayerViewModel(x)).ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            return View(new PlayerViewModel());
        }

        [HttpPost]
        public ActionResult Create(PlayerViewModel data)
        {
            var player = data.AsPlayer();

            ValidatePlayerInfo(player);

            if (!ModelState.IsValid)
                return View(data);

            _session.Add(player);
            _session.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Update(int playerId)
        {
            var player = _session.SingleById<Player>(playerId);

            if (player != null)
            {
                var model = new PlayerViewModel(player);

                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(PlayerViewModel data)
        {
            var player = data.AsPlayer();

            ValidatePlayerInfo(player);

            if (!ModelState.IsValid)
                return View(data);

            var original = _session.SingleById<Player>(data.Id);

            original.UpdateFrom(player);

            return RedirectToAction("Index");
        }


        private void ValidatePlayerInfo(Player data)
        {
            if (_session.Set<Player>().Any(x => x.Username == data.Username))
            {
                ModelState.AddModelError("Username", new Exception("A user with the same username already exists"));
            }
            else if (_session.Set<Player>().Any(x => x.Email == data.Email))
            {
                ModelState.AddModelError("Email", new Exception("A user with the same email already exists"));
            }
        }
    }
}