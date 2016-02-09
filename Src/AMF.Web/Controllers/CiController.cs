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
            _session.PURGEDATABASE();

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

            var arcane = BuildArcane();

            var divine = BuildDivine();

            var martial = BuildMartial();

            var chasse = BuildChasse();

            var nature = BuildNature();

            var roublard = BuildRoublard();


            var scenario = new Scenario();
            _session.Add(scenario);
            _session.Commit();

           
            var year = new Year
            {
                Name = "2016",
                Scenario = scenario,
                PlayableCategories = new List<Category> { arcane, divine, martial, chasse, nature, roublard },
                PlayableRaces = BuildRaces()
            };

            _session.Add(year);
            _session.Commit();


            var newEvent = new Event
            {
                Year = year,
                Date = new DateTime(2016, 01, 01),
                EventNumber = 0,
                NextEvent = true
            };
            year.Events.Add(newEvent);
            _session.Commit();

            return "Done";
        }

        private List<Race> BuildRaces()
        {
            var nains = new Race
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

            var humains = new Race
            {
                Name = "Humain",
            };
            var drake = new Race
            {
                Name = "Drake"
            };
            var elfe = new Race
            {
                Name = "Elfe"
            };

            var halfelin = new Race
            {
                Name = "Halfelin"
            };

            var orc = new Race
            {
                Name = "Orc"
            };

            var tarente = new Race
            {
                Name = "Tarente"
            };

            var duneen = new Race
            {
                Name = "Dunéen"
            };

            return new List<Race>
            {
                nains, halfelin, humains, duneen, drake, elfe, orc, tarente
            };

        }

        public Category BuildArcane()
        {
            var arcane = new Category
            {
                Name = "Arcane",
                IsMastery = false
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
                },
                new Skill
                {
                    Name = "Détection de la Magie",
                    IsPassive = true
                },
                new Skill
                {
                    Name = "Artificier",
                    IsPassive = true
                }

            };


            arcaneskills[9].Prerequisites = new List<Skill>
            {
                    arcaneskills[5],
                    arcaneskills[8],
                    arcaneskills[7],
            };

            arcaneskills[8].Prerequisites = new List<Skill>
            {
                    arcaneskills[6],
                    arcaneskills[3]
            };

            arcaneskills[7].Prerequisites = new List<Skill>
            {
                    arcaneskills[8],
                    arcaneskills[1],
                
            };

            arcaneskills[6].Prerequisites = new List<Skill>
            {
                    arcaneskills[0],
                    arcaneskills[1],
                    arcaneskills[2],
                
            };

            arcaneskills[5].Prerequisites = new List<Skill>
            {
                    arcaneskills[4]
                
            };

            arcaneskills[4].Prerequisites = new List<Skill>
            {
                    arcaneskills[3]
                
            };

            arcaneskills[3].Prerequisites = new List<Skill>
            {
                    arcaneskills[2]
            };

            arcane.Skills = arcaneskills;
            _session.Add(arcane);
            _session.Commit();

            return arcane;
        }

        private Category BuildDivine()
        {
            var divine = new Category
            {
                Name = "Divin",
                IsMastery = false
            };

            return divine;
        }

        private Category BuildNature()
        {
            var nature = new Category
            {
                Name = "Naturalisme",
                IsMastery = false
            };

            return nature;
        }

        private Category BuildMartial()
        {
            var martial = new Category
            {
                Name = "Martial",
                IsMastery = false
            };

            return martial;
        }

        public Category BuildChasse()
        {
            var chasse = new Category
            {
                Name = "Chasse",
                IsMastery = false
            };

            return chasse;
        }

        public Category BuildRoublard()
        {
            var roublard = new Category
            {
                Name = "Roublardise",
                IsMastery = false
            };

            return roublard;
        }
    }
}