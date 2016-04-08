using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AMF.Core.Enums;
using AMF.Core.Extensions;
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

            var models = chars
                .Where(x => !currentEvent.Attendees.Select(y => y.Id).Contains(x.Id))
                .Select(x => new CharacterViewModel(x)).ToList();

            RequireJsOptions.Add("eventNumber", currentEvent.EventNumber);

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

            var model = new CharacterViewModel();
            RequireJsOptions.Add("model", model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CharacterViewModel data)
        {
            if (!ModelState.IsValid)
                return View(data);

            var year = _session.Set<Year>().First(x => x.Current);

            var player = _session.SingleById<Player>(data.PlayerId);

            var skills = _session.Set<Skill>()
                .Where(x => data.SelectedSkills.Contains(x.Id))
                .ToList();

            var legacySkills = _session.Set<LegacySkill>()
                .Where(x => data.SelectedLegacySkills.Contains(x.Id))
                .ToList();

            var race = _session.SingleById<Race>(data.SelectedRace);

            var categories = _session.Set<Category>()
                .Where(x => data.SelectedCategories.Contains(x.Id))
                .ToList();

            var legacyTrees = new List<LegacyTree>();
            if (legacySkills.Any())
            {
                legacyTrees = _session.Set<LegacyTree>()
                    .Where(x => x.Skills
                        .Any(y => data.SelectedLegacySkills.Contains(y.Id)))
                    .ToList();
            }
            else
            {
                legacyTrees = categories.SelectMany(x => x.Legacies).ToList();
            }

            var currentEvent = year.Events.First(x => x.NextEvent);
            _session.Attach(currentEvent);

            var character = new Character
            {
                Player = player,
                Name = data.CharacterName,
                Race = race,
                Categories = categories,
                Skills = skills,
                Legacy = new Legacy
                {
                    LegacySkills = legacySkills,
                    LegacyAvailable = legacyTrees
                },
                Year = year,
                Presences = new List<Event> { currentEvent },
                Influence = 1 + (race.Skills.SelectMany(x => x.Bonus).Select(w => w.Bonus).Count(x => x == Bonus.ExtraInfluence) * 3),
                Experience = 1 + (race.Skills.SelectMany(x => x.Bonus).Select(w => w.Bonus).Count(x => x == Bonus.ExtraXP) * 5)
            };

            _session.Add(character);
            currentEvent.Attendees.Add(character);
            _session.Commit();

            return View("Debriefing", new DebriefingViewModel(character));
        }

        public ActionResult Update(int charId)
        {
            var character = _session.SingleById<Character>(charId);

            RequireJsOptions.Add("model", new
            {
                id = character.Id,
                name = character.Name,
                skills = character.Skills.Where(x => !x.IsPassive && !x.IsLegacy && !x.IsRacial).Select(x => x.Id).ToList(),
                categories = character.Categories.Select(x => x.Id),
                xp = character.Experience,
                race = character.Race.Id,
                player = character.Player.Id,
                legacies = character.Legacy != null ? character.Legacy.LegacySkills.Select(x => x.Id) : null,
                legaciesAvai = character.Legacy != null ? character.Legacy.LegacyAvailable.Select(x => x.Id) : null,
                title = character.Title
            });

            return View();
        }

        [HttpPost]
        public ActionResult Update(CharacterViewModel data)
        {
            var character = _session.SingleById<Character>(data.CharacterId);

            var skills = _session.Set<Skill>()
                .Where(x => data.SelectedSkills.Contains(x.Id))
                .ToList();

            character.Skills = skills;

            var cats = _session.Set<Category>()
                .Where(x => data.SelectedCategories.Contains(x.Id))
                .ToList();
            character.Categories = cats;

            _session.Commit();

            return View("Debriefing", new DebriefingViewModel(character));
        }

        public JsonResult GetDataForCurrentEvent()
        {
            var currentEvent = _session.Set<Event>().First(x => x.NextEvent);

            var currentYear = _session.Set<Year>().First(x => x.Current);

            var list = Enum.GetValues(typeof(Language)).Cast<Language>().ToList();

            var cats = currentYear.PlayableCategories.Where(x => x.Mastery != null)
                .Select(x => new
                {
                    catId = x.Id,
                    name = x.Name,
                    mastery = new
                    {
                        id = x.Mastery.Id,
                        name = x.Mastery.Name,
                        isMastery = x.Id,
                        skills = x.Mastery.Skills.Select(y => new
                        {
                            id = y.Id,
                            name = y.Name,
                            description = "",
                            bonus = y.Bonus,
                            isPassive = y.IsPassive,
                            isLegacy = y.IsLegacy,
                            armorRestricted = y.ArmorRestricted,
                            prereq = y.Prerequisites.Select(z => z.Id).ToList(),
                            categoryId = y.Id
                        }).ToList(),
                    },
                    skills = x.Skills.Where(z => !z.IsPassive && !z.IsLegacy && !z.IsRacial).Select(y => new
                    {
                        id = y.Id,
                        name = y.Name,
                        description = "",
                        bonus = y.Bonus,
                        isPassive = y.IsPassive,
                        isLegacy = y.IsLegacy,
                        armorRestricted = y.ArmorRestricted,
                        prereq = y.Prerequisites.Select(z => z.Id).ToList(),
                        categoryId = x.Id
                    }).ToList(),
                    passives = x.Skills.Where(z => z.IsPassive).Select(y => new
                    {
                        id = y.Id,
                        name = y.Name,
                        description = "",
                        bonus = y.Bonus,
                        isPassive = true,
                        armorRestricted = y.ArmorRestricted,
                        prereq = y.Prerequisites.Select(z => z.Id).ToList(),
                        categoryId = x.Id
                    }).ToList(),
                    legacies = x.Legacies.Select(y => new
                    {
                        id = y.Id,
                        skills = y.Skills.Select(z => new
                        {
                            id = z.Id,
                            prerequisites = z.Prerequisites.Select(w => w.Id).ToList(),
                            name = z.Name,
                            cost = z.Cost,
                            description = z.Description,
                            catId = x.Id
                        }).ToList(),
                    }).ToList()
                }).ToList();
            var races = currentYear.PlayableRaces.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                language = x.Language,
                skills = x.Skills.Select(y => new
                {
                    id = y.Id,
                    name = y.Name,
                    description = "",
                    bonus = y.Bonus,
                    category = y.Category != null ? y.Category.Id : 0,
                    isRacial = true,
                    armorRestricted = y.ArmorRestricted
                })
            }).ToList();
            var maxNbSkills = currentEvent.NbOfMaxSkill();
            var languagesAvailable = list.Select(x => new
            {
                text = x.AsDisplayable(),
                id = x
            }).ToList();

            var legacies = _session.Set<LegacyTree>().Select(x => new
            {
                id = x.Id,
                skills = x.Skills.Select(z => new
                {
                    id = z.Id,
                    prerequisites = z.Prerequisites.Select(w => w.Id).ToList(),
                    name = z.Name,
                    cost = z.Cost,
                    description = z.Description
                }).ToList(),
            }).ToList();

            return Json(new
            {
                cats = cats,
                races = races,
                maxNbSkills = maxNbSkills,
                maxNbCats = 1,
                languagesAvailable = languagesAvailable,
                legacies = legacies
            }, JsonRequestBehavior.AllowGet);
        }
    }
}