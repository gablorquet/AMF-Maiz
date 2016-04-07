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
    public class YearController : Controller
    {
        private readonly ISession _session;

        public YearController(ISession session)
        {
            _session = session;
        }

        // GET: Events
        public ActionResult Index()
        {
            var years = _session.Set<Year>().ToList();

            RequireJsOptions.Add("model", years);


            return View(years);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Create(YearViewModel model)
        {
            var events = new List<Event>();
            var currentDate = model.StartDate;
            var index = 0;
            while (currentDate < model.EndDate)
            {
                events.Add(new Event
                {
                    Date = currentDate,
                    EventNumber = index,
                });
                
                currentDate = currentDate.AddDays(7);
            }

            var scenario = new Scenario();
            if (model.ScenarioId.HasValue)
                scenario = _session.SingleById<Scenario>(model.ScenarioId.Value);
            else
                scenario.Name = model.ScenarioName;


            var year = new Year
            {
                Events = events,
                Name = model.YearName,
                PlayableCategories = _session.Set<Category>().ToList(),
                PlayableRaces = _session.Set<Race>().ToList()
                
            };
            scenario.Years.Add(year);
            _session.Add(scenario);
            _session.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Update(int yearId)
        {
            var data = _session.SingleById<Year>(yearId);

            RequireJsOptions.Add("year", data);
            
            return View();
        }

        public ActionResult Update(YearViewModel model)
        {
            var data = _session.SingleById<Year>(model.YearId);

            data.Current = model.Current;
            
            _session.Commit();
            
            return RedirectToAction("Update", new { eventId = data.Id });
        }

        public ActionResult CloseEvent(int scenarioId)
        {
            var data = _session.SingleById<Year>(scenarioId);

            data.Current = false;
            
            _session.Commit();
            
            return RedirectToAction("YearStats", new { eventId = data.Id });
        }
    }
}