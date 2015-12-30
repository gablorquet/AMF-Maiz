using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AMF.Core.Extensions;
using AMF.Core.Model;
using AMF.Core.Storage;

namespace AMF.Web.Controllers
{
    public class CiController : Controller
    {
        private readonly ISession _session;

        public CiController(ISession session)
        {
            _session = session;
        }

        public string Clear()
        {
            var characters = _session.Set<Character>().ToList();
            foreach (var character in characters)
            {
                _session.Delete(character);
            }

            var users = _session.Set<User>().ToList();
            foreach (var user in users)
            {
                _session.Delete(user);
            }

            var skills = _session.Set<Skill>().ToList();
            foreach (var skill in skills)
            {
                _session.Delete(skill);
            }

            var spells = _session.Set<Spell>().ToList();
            foreach (var spell in spells)
            {
                _session.Delete(spell);
            }

            var categories = _session.Set<Category>().ToList();
            foreach (var category in categories)
            {
                _session.Delete(category);
            }

            var races = _session.Set<Race>().ToList();
            foreach (var race in races)
            {
                _session.Delete(race);
            }

            var events = _session.Set<Event>().ToList();
            foreach (var eventitem in events)
            {
                _session.Delete(eventitem);
            }

            var years = _session.Set<Year>().ToList();
            foreach (var year in years)
            {
                _session.Delete(year);
            }

            _session.Commit();

            return "Done";

        }

        public string Populate()
        {
            var animateur = new Animateur
            {
                Email = "gablorquet@gmail.com",
                FirstName = "Gab",
                LastName = "Lorquet",
                Username = "GabLork",
                Password = "asdf".ToSHA1()
            };

            _session.Add(animateur);
            _session.Commit();

            var arcane = new Category
            {
                Name = "Arcane",
                IsMastery = false
            };

            _session.Add(arcane);
            _session.Commit();


            var arcanePassives = new List<Skill>
            {
                new Skill
                {
                    Name = "Détection de la Magie",
                },
                new Skill
                {
                    Name = "Artificier"
                }
            };

            var arcaneskills = new List<Skill>
            {
                new Skill //0
                {
                    Name = "+1 Bâton Mineur",
                },
                new Skill //1
                {
                    Name = "Parchemin"
                },
                new Skill //2
                {
                    Name  = "Sorts Mineurs 1",
                },
                new Skill //3
                {
                    Name  = "Sorts Mineurs 2",
                },
                new Skill //4
                {
                    Name  = "Sorts Majeurs 1",
                },
                new Skill //5
                {
                    Name  = "Sorts Majeurs 2",
                },
                new Skill //6
                {
                    Name = "Maitrise 1 : Chaîne / Flèche"
                },
                new Skill //7
                {
                    Name = "Maîtrise 2 : Boule / Flèche",
                    ArmorRestricted = true,
                },
                new Skill //8
                {
                    Name = "Maîtrise 3 : Choc / Chaine",
                    ArmorRestricted = true,
                },                
                new Skill //9
                {
                    Name = "Incantation Rapide",
                    ArmorRestricted = true,
                }
            };


            arcaneskills[9].Prerequisites = new List<Skill>
            {
                    arcaneskills[5],
                    arcaneskills[8],
                    arcaneskills[7],
            };
            arcane.Skills = arcaneskills;

            _session.Add(arcane);
            _session.Commit();

            //arcaneskills[8].Prerequisite = new SkillsPrerequisite
            //{
            //    Parent = arcaneskills[8],
            //    Child = new List<Skill>
            //    {
            //        arcaneskills[6],
            //        arcaneskills[3]
            //    }
            //};
            //_session.Commit();

            //arcaneskills[7].Prerequisite = new SkillsPrerequisite
            //{
            //    Parent = arcaneskills[7],
            //    Child = new List<Skill>
            //    {
            //        arcaneskills[8],
            //        arcaneskills[1],
            //    }
            //};
            //_session.Commit();

            //arcaneskills[6].Prerequisite = new SkillsPrerequisite
            //{
            //    Parent = arcaneskills[6],
            //    Child = new List<Skill>
            //    {
            //        arcaneskills[0],
            //        arcaneskills[1],
            //        arcaneskills[2],
            //    }
            //};
            //_session.Commit();

            //arcaneskills[5].Prerequisite = new SkillsPrerequisite
            //{
            //    Parent = arcaneskills[5],
            //    Child = new List<Skill>
            //    {
            //        arcaneskills[4]
            //    }
            //};
            //_session.Commit();

            //arcaneskills[4].Prerequisite = new SkillsPrerequisite
            //{
            //    Parent = arcaneskills[4],
            //    Child = new List<Skill>
            //    {
            //        arcaneskills[3]
            //    }
            //};
            //_session.Commit();

            //arcaneskills[3].Prerequisite = new SkillsPrerequisite
            //{
            //    Parent = arcaneskills[3],
            //    Child = new List<Skill>
            //    {
            //        arcaneskills[2]
            //    }
            //};

            _session.Commit();
            
            var scenario = new Scenario();
            _session.Add(scenario);
            _session.Commit();


            var race = new Race
            {
                Name = "Nains",
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "Port d'armure lourde"
                    },
                    new Skill
                    {
                        Name = "Soins amélioré"
                    }
                }
            };
            _session.Add(race);
            _session.Commit();

            var year = new Year
            {
                Name = "2016",
                Scenario = scenario,
                PlayableCategories = new List<Category> { arcane },
                PlayableRaces = new List<Race> {  race, }
            };

            _session.Add(year);
            _session.Commit();


            var newEvent = new Event
            {
                Year = year,
                Date = new DateTime(2016,01,01),
                EventNumber = 0
            };
            year.Events.Add(newEvent);
            _session.Add(newEvent);
            _session.Commit();

            return "Done";
        }
    }
}