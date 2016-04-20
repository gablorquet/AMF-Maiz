using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AMF.Core.Enums;
using AMF.Core.Extensions;
using AMF.Core.Model;
using AMF.Core.Storage;
using AMF.Web.Areas.Admin.ViewModels;
using RequireJsNet;

namespace AMF.Web.Areas.Admin.Controllers
{
    public class EventController : Controller
    {
        private readonly ISession _session;

        public EventController(ISession session)
        {
            _session = session;
        }

        // GET: Events
        public ActionResult Index()
        {
            var year = _session
                .Set<Year>()
                .First(x => x.Current);

            var events = _session.Set<Year>()
                .Where(x => x.Current)
                .SelectMany(x => x.Events)
                .ToList();

            RequireJsOptions.Add("model", events.Select(x => new
            {
                eventId = x.Id,
                yearId = year.Id,
                date = x.Date,
                closed = x.ClosedDate,
                eventNumber = x.EventNumber,
                isNext = x.NextEvent
            }));


            return View(events);
        }

        public ActionResult CancelEvent(int eventId)
        {
            var data = _session.SingleById<Event>(eventId);

            data.WasCanceled = true;
            _session.Commit();

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Stats(int eventId)
        {
            var data = _session.SingleById<Event>(eventId);

            var model = GetStats(data);

            RequireJsOptions.Add("model", model);

            return View("EventStats");
        }

        public ActionResult CloseEvent(int eventId)
        {
            var data = _session.SingleById<Event>(eventId);

            var model = GetStats(data);

            data.ClosedDate = DateTime.Now;
            data.NextEvent = false;

            var events = _session.Set<Year>()
                .Where(x => x.Current).SelectMany(x => x.Events).ToList();

            var next = events.ElementAt(events.IndexOf(data) +1);
            next.NextEvent = true;

            _session.Commit();

            RequireJsOptions.Add("model", model);

            return View("EventStats");
        }

        public ActionResult Debriefing()
        {
            var currentEvent = _session.Set<Event>()
                .First(x => x.NextEvent);
 
            var model = new
            {
                attendees = currentEvent.Attendees.Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    playerName = x.Player.FullName,
                    displayName = x.Name + " - " + x.Player.FullName
                })
            };

            RequireJsOptions.Add("model", model);

            return View();
        }

        [HttpPost]
        public ActionResult PlayerDebrief(int characterId, int nbThrophies)
        {
            var character = _session.SingleById<Character>(characterId);

            for (var i = 0; i < nbThrophies; i++)
            {
                character.Throphies.Add(new Throphy());
            }

            _session.Commit();

            return RedirectToAction("Debriefing");
        }



        private StatsViewModel GetStats(Event data)
        {
            var catAvail = _session.Set<Year>()
                .Where(x => x.Current)
                .SelectMany(x => x.PlayableCategories)
                .ToList();

            var categories = new List<CatStatsViewModel>();
            foreach (var category in catAvail)
            {
                var count = data.Attendees.Count(x => x.Categories.Select(y => y.Id).Contains(category.Id));
                var catdata = new CatStatsViewModel
                {
                    name = category.Name,
                    count = count,
                    percentage = ((int)count / data.Attendees.Count) * 100
                };

                categories.Add(catdata);
            }

            var model = new StatsViewModel
            {
                eventNumber = data.EventNumber,
                attendees = data.Attendees.Count,
                ageAvg = data.Attendees
                    .Where(x => x.Player.DateOfBirth.HasValue)
                    .Select(x => x.Player.DateOfBirth.Value.AsAge())
                    .Average(),

                categories = categories,
            };

            return model;
        }


    }
}