﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using AMF.Core.Enums;
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
                Password = "asdf".ToSHA1(),
            };
            _session.Add(animateur);

            var joueur = new Player
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                Username = "Test",
                DateOfBirth = new DateTime(2000, 01, 01)
            };
            _session.Add(joueur);
            _session.Commit();

            BuildArcane();
            BuildDivine();
            BuildNature();
            BuildMartial();
            BuildChasse();
            BuildRoublard();

            BuildRaces(_session.Set<Category>().ToList());

            BuildEvents();

            var sc = new Scenario
            {
                Name = "Guerre Mazérienne",
                Years = new List<Year>
                {
                    new Year
                    {
                        Name = "2016",
                        Current = true,
                        PlayableCategories = _session.Set<Category>().ToList(),
                        PlayableRaces = _session.Set<Race>().ToList(),
                        Events = _session.Set<Event>().ToList()
                    }
                }
            };

            _session.Add(sc);
            _session.Commit();

            return _session.Set<Event>().Count().ToString();
        }

        private void BuildEvents()
        {
            var currentDate = new DateTime(2016, 05, 06);
            var endDate = new DateTime(2016, 09, 09);
            var index = 0;
            while (currentDate < endDate)
            {
                var ev = new Event
                {
                    Date = currentDate,
                    EventNumber = index,
                    NextEvent = index == 0,
                    WasCanceled = false,
                };

                _session.Add(ev);
                _session.Commit();

                currentDate = currentDate.AddDays(7);
                index++;
            }
        }

        private void BuildRaces(List<Category> cats)
        {
            var arcane = cats.First(x => x.Name == "Arcane");
            var divine = cats.First(x => x.Name == "Divin");
            var nat = cats.First(x => x.Name == "Naturalisme");
            var martial = cats.First(x => x.Name == "Martial");
            var rouge = cats.First(x => x.Name == "Roublardise");
            var chasse = cats.First(x => x.Name == "Chasse");

            var nains = new Race
            {
                Name = "Nain",
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "Port d'armure lourde",
                        IsRacial = true,
                        Bonus = new List<SkillBonus> { new SkillBonus
                        {
                         Bonus = Bonus.HeavyArmorProf    
                        } }
                    },
                    new Skill
                    {
                        Name = "Soins amélioré sur soi",
                        IsRacial = true,
                        Category = divine
                    }
                },
                Language = Language.Draconique
            };

            var humains = new Race
            {
                Name = "Humain",
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Bonus = new List<SkillBonus> {new SkillBonus
                        {
                            Bonus = Bonus.ExtraGoldFromInfluence
                        }},
                        Name = "+1 Pièces d'or",
                        IsRacial = true
                    },
                    new Skill
                    {
                        Name = "Polyvalence",
                        Bonus = new List<SkillBonus>
                        {
                            new SkillBonus { Bonus = Bonus.ExtraXP }
                        },
                        IsRacial = true
                    },
                },
                Language = Language.Commun
            };
            var drake = new Race
            {
                Name = "Drake",
                Language = Language.Draconique,
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "Recharge (15 minutes)",
                        IsRacial = true,
                        Category = arcane
                    },
                    new Skill
                    {
                        Name = "+3 Influence",
                        Bonus = new List<SkillBonus> {new SkillBonus
                        {
                            Bonus = Bonus.ExtraInfluence
                        }},
                        IsRacial = true
                    }
                }
            };
            var elfe = new Race
            {
                Name = "Elfe",
                Language = Language.Elfique,
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "Ambidextre",
                        IsRacial = true
                    },
                    new Skill
                    {
                        Name = "Arme de prédilectiion",
                        IsRacial = true,
                        Category = chasse
                    }
                }
            };

            var halfelin = new Race
            {
                Name = "Halfelin",
                Language = Language.All,
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "+6 Influence",
                        Bonus = new List<SkillBonus> {new SkillBonus
                        {
                         Bonus   = Bonus.ExtraInfluence
                        }, new SkillBonus
                        {
                         Bonus   = Bonus.ExtraInfluence
                        }},
                        IsRacial = true
                    }
                }
            };

            var orc = new Race
            {
                Name = "Orc",
                Language = Language.Goblinoide,
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "Port d'arme lourde",
                        Bonus = new List<SkillBonus>
                        {
                            new SkillBonus
                            {
                                Bonus = Bonus.HeavyWeaponProf
                            }
                        },
                        IsRacial = true
                    },
                    new Skill
                    {
                        Name = "+1 PV",
                        IsRacial = true,
                        Bonus = new List<SkillBonus> {new SkillBonus
                        {
                         Bonus   = Bonus.ExtraHP
                        }},
                        Category = nat
                    }
                }
            };

            //var tarente = new Race
            //{
            //    Name = "Tarente",
            //    Language = Language.Profondeur,
            //    Skills = new List<Skill>
            //    {
            //        new Skill
            //        {
            //            Name = "Nécrophage",
            //            IsRacial = true
            //        },
            //        new Skill
            //        {
            //            Name = "+1 PV",
            //            Bonus = new List<SkillBonus> {new SkillBonus
            //            {
            //             Bonus   = Bonus.ExtraHP
            //            }},
            //            IsRacial = true,
            //            Category = chasse
            //        }
            //    }
            //};

            var duneen = new Race
            {
                Name = "Dunéen",
                Language = Language.Commun,
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "+1 PV",
                        Bonus = new List<SkillBonus> {new SkillBonus
                        {
                         Bonus   = Bonus.ExtraHP
                        }},
                        IsRacial = true
                    },
                    new Skill
                    {
                        Name = "+1 Bâton mineur",
                        Bonus = new List<SkillBonus> {new SkillBonus
                        {
                         Bonus   = Bonus.ExtraMinor
                        }},
                        IsRacial = true,
                        Category = arcane
                    }
                }
            };

            _session.Add(nains);
            _session.Add(halfelin);
            _session.Add(humains);
            _session.Add(duneen);
            _session.Add(drake);
            _session.Add(elfe);
            _session.Add(orc);
            _session.Add(orc);

        }

        public Category BuildArcane()
        {
            var arcane = new Category
            {
                Name = "Arcane",
            };

            var mastery = new Category
            {
                Name = "Arcane (Maîtrise)",
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "Posture : Mage de Guerre",
                        IsPassive = true,
                    }
                }
            };
            arcane.Mastery = mastery;

            var legacy = new List<LegacyTree>
            {
                new LegacyTree
                {
                    Skills = new List<LegacySkill>
                    {
                        new LegacySkill
                        {
                            Name = "Test 1",
                            Cost = 5
                        },
                        new LegacySkill
                        {
                            Name = "Test 2",
                            Cost = 7
                        },
                        new LegacySkill
                        {
                            Name = "Test 3",
                            Cost = 10
                        }
                    }
                },    
                new LegacyTree
                {
                    Skills = new List<LegacySkill>
                    {
                        new LegacySkill
                        {
                            Name = "Test 4",
                            Cost = 5
                        },
                        new LegacySkill
                        {
                            Name = "Test 5",
                            Cost = 7
                        },
                        new LegacySkill
                        {
                            Name = "Test 6",
                            Cost = 10
                        }
                    }
                }
            };

            legacy.First().Skills.ElementAt(1).Prerequisites = new List<LegacySkill> { legacy.First().Skills.ElementAt(0) };
            legacy.First().Skills.ElementAt(2).Prerequisites = new List<LegacySkill> { legacy.First().Skills.ElementAt(1) };

            legacy.Last().Skills.ElementAt(1).Prerequisites = new List<LegacySkill> { legacy.Last().Skills.ElementAt(0) };
            legacy.Last().Skills.ElementAt(2).Prerequisites = new List<LegacySkill> { legacy.Last().Skills.ElementAt(1) };

            arcane.Legacies = legacy;

            var arcaneskills = new List<Skill>
            {
                new Skill //0
                {
                    Name = "+1 Bâton Mineur",
                    Bonus = new List<SkillBonus> {new SkillBonus
                    {
                     Bonus = Bonus.ExtraMinor   
                    }},
                },
                new Skill //1
                {
                    Name = "Parchemin"
                },
                new Skill //2
                {
                    Name  = "Sorts Mineurs 1",
                    Bonus = new List<SkillBonus> { new SkillBonus
                    {
                     Bonus   = Bonus.ThreeMinor
                    }}
                },
                new Skill //3
                {
                    Name  = "Sorts Mineurs 2",
                    Bonus = new List<SkillBonus> { new SkillBonus
                    {
                     Bonus   = Bonus.ThreeMinor
                    }}
                },
                new Skill //4
                {
                    Name  = "Sorts Majeurs 1",
                    Bonus = new List<SkillBonus> { new SkillBonus
                    {
                        Bonus = Bonus.TwoMajor
                    }}
                },
                new Skill //5
                {
                    Name  = "Sorts Majeurs 2",
                    Bonus = new List<SkillBonus> { new SkillBonus
                    {
                        Bonus = Bonus.TwoMajor
                    }}
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
                Mastery = new Category
                {
                    Name = "Divin (Maîtrise)",
                    Skills = new List<Skill>
                    {
                        new Skill
                        {
                            Name = "+1 Point de Destin",
                            IsPassive = true,
                            Bonus = new List<SkillBonus>
                            {
                                new SkillBonus
                                {
                                    Bonus = Bonus.ExtraDestin
                                }
                            }
                        }
                    }
                }
            };

            var divineSkills = new List<Skill>
            {
                new Skill //0
                {
                    Name = "Immunité au Silence"
                },
                new Skill
                {
                    Name = "Parler à un mort" //1
                },
                new Skill
                {
                    Name = "Sorts Mineurs 1", //2
                    Bonus = new List<SkillBonus>{new SkillBonus
                    {
                     Bonus = Bonus.ThreeMinor   
                    }}
                },
                new Skill
                {
                    Name = "Sorts Mineurs 2" //3
                },
                new Skill
                {
                    Name = "Souffle Divin" //4
                },
                new Skill
                {
                    Name    = "Sorts Majeurs 1", //5
                    Bonus = new List<SkillBonus>{new SkillBonus
                    {
                     Bonus   = Bonus.TwoMajor
                    }}
                },
                new Skill
                {
                    Name = "Sorts Majeurs 2", //6
                    Bonus = new List<SkillBonus>{new SkillBonus
                    {
                     Bonus   = Bonus.TwoMajor
                    }}
                },
                new Skill
                {
                    Name = "Soin (+1 PV)", //7
                    ArmorRestricted = true
                },
                new Skill
                {
                    Name = "Guérison Instantanée", //8
                    ArmorRestricted = true
                },
                new Skill
                {
                    Name = "Grand Prêtre", //9
                    ArmorRestricted = true,
                    Bonus = new List<SkillBonus>
                    {
                        new SkillBonus
                        {
                            Bonus = Bonus.ExtraMinor
                        },
                        new SkillBonus
                        {
                            Bonus = Bonus.ExtraMajor
                        }
                    }
                }

            };

            divineSkills[4].Prerequisites = new List<Skill>
            {
                divineSkills[0], divineSkills[1], divineSkills[2]
            };

            divineSkills[3].Prerequisites = new List<Skill>
            {
                divineSkills[2]
            };

            divineSkills[7].Prerequisites = new List<Skill>
            {
                divineSkills[4], divineSkills[3]
            };

            divineSkills[8].Prerequisites = new List<Skill>
            {
                divineSkills[4], divineSkills[7]
            };

            divineSkills[5].Prerequisites = new List<Skill>
            {
                divineSkills[3]
            };

            divineSkills[6].Prerequisites = new List<Skill>
            {
                divineSkills[5]
            };
            divineSkills[9].Prerequisites = new List<Skill>
            {
                divineSkills[6], divineSkills[7]
            };

            divine.Skills = divineSkills;


            _session.Add(divine);
            _session.Commit();

            return divine;
        }

        private Category BuildNature()
        {
            var nature = new Category
            {
                Name = "Naturalisme",
                Mastery = new Category
                {
                    Name = "Naturalisme (Maîtrise)",
                    Skills = new List<Skill>
                    {
                        new Skill
                        {
                            Name = "Ritualiste",
                            IsPassive = true,
                        }
                    }
                }
            };

            var natSkills = new List<Skill>
            {
                new Skill
                {
                    Name = "Grâce animale", //0
                },
                new Skill
                {
                    Name = "Sorts Mineurs 1", //1
                    Bonus = new List<SkillBonus>{new SkillBonus
                    {
                     Bonus   = Bonus.ThreeMinor
                    }}
                },
                new Skill
                {
                    Name = "Armure Magique" //2
                },
                new Skill
                {
                    Name = "Sorts Mineurs 2", //3
                    Bonus = new List<SkillBonus>{ new SkillBonus
                    {
                        Bonus = Bonus.ThreeMinor
                    }}
                },
                new Skill
                {
                    Name = "Amélioration 1 : Immobilisation / Projection" //4
                },
                new Skill
                {
                    Name = "Sorts Majeurs 1", //5
                    Bonus = new List<SkillBonus>{new SkillBonus
                    {
                        Bonus = Bonus.TwoMajor
                    }}
                },
                new Skill
                {
                    Name = "Sorts Majeurs 2", //6
                    Bonus = new List<SkillBonus>{new SkillBonus
                    {
                     Bonus   = Bonus.TwoMajor
                    }}
                },
                new Skill
                {
                    Name = "Mutation animalière : +2 PV", //7
                    ArmorRestricted = true
                },
                new Skill
                {
                    Name = "Armure Physique", //8
                    ArmorRestricted = true
                },
                new Skill
                {
                    Name = "Amélioration 2 : Silence / Dissimulation", //9
                    ArmorRestricted = true
                }
            };


            natSkills[2].Prerequisites = new List<Skill>
            {
                natSkills[0]
            };

            natSkills[4].Prerequisites = new List<Skill>
            {
                natSkills[0], natSkills[1]
            };

            natSkills[3].Prerequisites = new List<Skill>
            {
                natSkills[1]
            };

            natSkills[5].Prerequisites = new List<Skill>
            {
                natSkills[3]
            };

            natSkills[6].Prerequisites = new List<Skill>
            {
                natSkills[5]
            };

            natSkills[7].Prerequisites = new List<Skill>
            {
                natSkills[3], natSkills[4]
            };

            natSkills[8].Prerequisites = new List<Skill>
            {
                natSkills[7]
            };

            natSkills[9].Prerequisites = new List<Skill>
            {
                natSkills[6], natSkills[7]
            };

            nature.Skills = natSkills;


            _session.Add(nature);
            _session.Commit();

            return nature;
        }

        private Category BuildMartial()
        {
            var martial = new Category
            {
                Name = "Martial",
                Mastery = new Category
                {
                    Name = "Martial (Maîtrise)",
                    Skills = new List<Skill>
                    {
                        new Skill
                        {
                            Name = "+1 PV",
                            Bonus = new List<SkillBonus>
                            {
                                new SkillBonus
                                {
                                    Bonus = Bonus.ExtraHP
                                }
                            },
                            IsPassive = true
                        }
                    }
                }
            };

            var martialSkill = new List<Skill>
            {
                new Skill //0
                {
                    Name = "Armes Lourdes (+1 dégât)",
                    Prerequisites = new List<Skill>()
                },
                new Skill //1
                {
                    Name = "Charge brise-bouclier",
                    Prerequisites = new List<Skill>()
                }, 
                new Skill //2
                {
                    Name = "Armes à deux mains (+1 dégât)",
                    Prerequisites = new List<Skill>()
                },
                new Skill //3
                {
                    Name = "Charge (+1 dégât)"
                },
                new Skill //4
                {
                    Name = "Proie : Frappe Passe-Armure",
                    Prerequisites = new List<Skill>()
                },
                new Skill //5
                {
                    Name = "Armes longues (+1 dégât)",
                    Prerequisites = new List<Skill>()
                },
                new Skill //6
                {
                    Name = "Coup Ralenti",
                    Prerequisites = new List<Skill>()
                },
                new Skill //7
                {
                    Name = "Proie Esquive"
                },

                new Skill //8
                {
                    Name = "Posture : Coup projetant",
                    Prerequisites = new List<Skill>()
                },
                new Skill //9
                {
                    Name = "Posture : Réduction de dégâts (1)",
                    Prerequisites = new List<Skill>()
                },
                new Skill //10
                {
                    Name = "Réduction de dégâts à distance",
                    Prerequisites = new List<Skill>()
                },
                new Skill //11
                {
                    Name = "Posture : Immunité aux projections"
                }
            };

            martialSkill[0].Prerequisites = new List<Skill>
            {
                martialSkill[1]
            };
            martialSkill[1].Prerequisites = new List<Skill>
            {
                martialSkill[2]
            };
            martialSkill[2].Prerequisites = new List<Skill>
            {
                martialSkill[3]
            };


            martialSkill[4].Prerequisites = new List<Skill>
            {
                martialSkill[5]
            };
            martialSkill[5].Prerequisites = new List<Skill>
            {
                martialSkill[6]
            };
            martialSkill[6].Prerequisites = new List<Skill>
            {
                martialSkill[7]
            };


            martialSkill[8].Prerequisites = new List<Skill>
            {
                martialSkill[9]
            };
            martialSkill[9].Prerequisites = new List<Skill>
            {
                martialSkill[10]
            };
            martialSkill[10].Prerequisites = new List<Skill>
            {
                martialSkill[11]
            };
            martial.Skills = martialSkill;

            _session.Add(martial);
            _session.Commit();

            return martial;
        }

        public Category BuildChasse()
        {
            var chasse = new Category
            {
                Name = "Chasse",
                Mastery = new Category
                {
                    Name = "Chasse (Maîtrise)",
                    Skills = new List<Skill>
                    {
                        new Skill
                        {
                            Name = "Maître Chasseur",
                            IsPassive = true
                        }
                    }
                }
            };

            var chasseSkill = new List<Skill>
            {
                new Skill
                {
                    Name = "Piège naturel" //0
                },
                new Skill
                {
                    Name = "Premiers Soins" //1
                },
                new Skill
                {
                    Name = "Arc / Arbalète (+1 dégât)" //2
                },
                new Skill
                {
                    Name = "Survie" //3
                },
                new Skill
                {
                    Name = "Camouflage naturel" //4
                },
                new Skill
                {
                    Name = "Armes légères (+1 dégât)" //5
                },
                new Skill
                {
                    Name = "Immunité aux sournoiseries" //6
                },
                new Skill
                {
                    Name = "Arc / Arbalète (+1 Passe-armure)" //7
                },
                new Skill
                {
                    Name = "Proie : Science du désarmement" //8
                },
                new Skill
                {
                    Name = "Proie : Réduction de dégâts (1)" //9
                },
                new Skill
                {
                    Name = "Camouflage partagé (+2 personnes)" //10
                },
                new Skill
                {
                    Name = "Embuscade Sournoise" //11
                }
            };

            chasseSkill[3].Prerequisites = new List<Skill>
            {
                chasseSkill[0], chasseSkill[1]
            };

            chasseSkill[4].Prerequisites = new List<Skill>
            {
                chasseSkill[1], chasseSkill[2]
            };

            chasseSkill[5].Prerequisites = new List<Skill>
            {
                chasseSkill[3]
            };

            chasseSkill[6].Prerequisites = new List<Skill>
            {
                chasseSkill[3], chasseSkill[4]
            };

            chasseSkill[7].Prerequisites = new List<Skill>
            {
                chasseSkill[4]
            };

            chasseSkill[8].Prerequisites = new List<Skill>
            {
                chasseSkill[5]
            };
            chasseSkill[9].Prerequisites = new List<Skill>
            {
                chasseSkill[5]
            };

            chasseSkill[10].Prerequisites = new List<Skill>
            {
                chasseSkill[7]
            };
            chasseSkill[11].Prerequisites = new List<Skill>
            {
                chasseSkill[7]
            };

            chasse.Skills = chasseSkill;

            _session.Add(chasse);
            _session.Commit();

            return chasse;
        }

        public Category BuildRoublard()
        {
            var roublard = new Category
            {
                Name = "Roublardise",
                Mastery = new Category
                {
                    Name = "Roublardise (Maîtrise)",
                    Skills = new List<Skill>
                    {
                        new Skill
                        {
                            Name = "Maître des Poisons",
                            IsPassive = true
                        }
                    }
                }
            };

            var rogueSkill = new List<Skill>
            {
                new Skill
                {
                    Name = "Coup Sournois" //0
                },
                new Skill
                {
                    Name = "Science de la dissimulation d'objets" //1
                },
                new Skill
                {
                    Name = "Science de la simulation de la mort" //2
                },
                new Skill
                {
                    Name = "Corps pièges" //3
                },
                new Skill
                {
                    Name = "Voleur expert" //4
                },
                new Skill
                {
                    Name = "Maître Assasssin" //5
                },
                new Skill
                {
                    Name = "Armes courtes & de jet passe-armure" //6
                },
                new Skill
                {
                    Name = "Science de l'interrogation" //7
                },
                new Skill
                {
                    Name = "Coupe gorge sournois" //8
                },
                new Skill
                {
                    Name = "Écran de fumée naturel" //9
                },
                new Skill
                {
                    Name = "Disparition naturelle" //10
                },
                new Skill
                {
                    Name = "Coup inapte" //11
                }
            };

            rogueSkill[3].Prerequisites = new List<Skill>
            {
                rogueSkill[0], rogueSkill[1]
            };

            rogueSkill[4].Prerequisites = new List<Skill>
            {
                rogueSkill[1], rogueSkill[2]
            };

            rogueSkill[5].Prerequisites = new List<Skill>
            {
                rogueSkill[3]
            };

            rogueSkill[6].Prerequisites = new List<Skill>
            {
                rogueSkill[3], rogueSkill[4]
            };

            rogueSkill[7].Prerequisites = new List<Skill>
            {
                rogueSkill[4]
            };

            rogueSkill[8].Prerequisites = new List<Skill>
            {
                rogueSkill[5]
            };
            rogueSkill[9].Prerequisites = new List<Skill>
            {
                rogueSkill[5]
            };

            rogueSkill[10].Prerequisites = new List<Skill>
            {
                rogueSkill[7]
            };
            rogueSkill[11].Prerequisites = new List<Skill>
            {
                rogueSkill[7]
            };

            roublard.Skills = rogueSkill;

            _session.Add(roublard);
            _session.Commit();

            return roublard;
        }
    }
}