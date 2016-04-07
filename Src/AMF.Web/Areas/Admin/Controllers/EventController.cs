using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
            var year = _session.Set<Year>()
                .Where(x => x.Current);

            var events = _session.Set<Year>()
                .Where(x => x.Current)
                .SelectMany(x => x.Events)
                .ToList();

            RequireJsOptions.Add("model", events.Select(x => new
            {
                eventId = x.Id,
                yearId = x.Year.Id,
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

        public ActionResult CloseEvent(int eventId)
        {
            var data = _session.SingleById<Event>(eventId);

            var catAvail = _session.Set<Year>()
                .Where(x => x.Current)
                .SelectMany(x => x.PlayableCategories)
                .ToList();

            var categories = new List<dynamic>();
            foreach (var category in catAvail)
            {
                var count = data.Attendees.Count(x => x.Categories.Select(y => y.Id).Contains(category.Id));
                dynamic catdata = new
                {
                    name = category.Name,
                    count = count,
                    percentage = ((int)count / data.Attendees.Count) * 100
                };

                categories.Add(catdata);
            }

            var model = new
            {
                eventNumber = data.EventNumber,
                attendees = data.Attendees.Count,
                ageAvg = data.Attendees
                    .Where(x => x.Player.DateOfBirth.HasValue)
                    .Select(x => x.Player.DateOfBirth.Value.AsAge())
                    .Average(),

                categories = categories,
            };

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
    }
}