require(
    [
        "jquery",
        "knockout",
        "lazy",
        "bootstrap"
    ],
    function ($, ko, Lazy) {
        var data;
        $.ajax("/Admin/Character/GetDataForCurrentEvent")
            .done(function (resp) {
                data = resp;

                var model = {};
                model.CreateCharacter = function () {
                    var self = this;

                    self.characterName = ko.observable().extend({ notify: 'always' });
                    self.characterName('');

                    self.languagesAvailable = ko.observableArray(data.languagesAvailable);
                    self.selectedLanguage = ko.observable().extend({ notify: 'always' });

                    self.racesAvailable = data.races;
                    self.selectedRace = ko.observable().extend({ notify: 'always' });
                    self.selectedRace.subscribe(function () {
                        var selected = self.selectedRace();

                        var newSkills = [];

                        selected.skills.forEach(function (racial) {
                            if (racial.category) {
                                if (Lazy(self.skills.unlockedCats()).some(function(cat) {
                                    return cat.catId == racial.category;
                                })) {
                                    newSkills.push(racial);
                                }
                            } else {
                                newSkills.push(racial);
                            }
                        });

                        self.selectedRacials(newSkills);

                        var filtered = Lazy(self.languagesAvailable()).where(function (lng) {
                            if (selected.language === 3)
                                return false;

                            return lng.id !== selected.language;
                        }).toArray();
                        self.languagesAvailable(filtered);
                    });

                    self.selectedRacials = ko.observableArray();

                    self.legacySkills = ko.observableArray();
                    self.legacyAvail = function (legacySkill) {

                        var catUnlockCond = !!Lazy(self.skills.unlockedCats()).find(function (unlocked) {
                            return unlocked.catId === legacySkill.catId;
                        });

                        if (!catUnlockCond)
                            return false;

                        var hasBonusSkills = Lazy(self.selectedRace().skills)
                            .find(function (r) {
                                return Lazy(r.bonus).some(function (b) {
                                    return b.Bonus === 8;
                                });
                            });

                        if (!hasBonusSkills)
                            return false;

                        if (legacySkill.prerequisites.length !== 0)
                            return false;

                        if (self.legacySkills().length === 1)
                            return false;

                        return true;
                    };

                    self.skills = new function () {
                        var skills = {};
                        skills.categories = data.cats;
                        skills.unlockedCats = ko.observableArray([]);
                        skills.unlockedCats.subscribe(function () {
                            var passives = Lazy(data.cats).pluck('passives').flatten().where(function (passive) {
                                return Lazy(skills.unlockedCats()).some(function (cat) {
                                    return cat.catId === passive.categoryId;
                                });
                            }).toArray();

                            self.selectedRace.notifySubscribers();

                            self.legacySkills.notifySubscribers();

                            skills.selectedPassives(passives);
                        });

                        skills.nbCatChoiceLeft = ko.pureComputed(function () {
                            return data.maxNbCats - skills.unlockedCats().length;
                        });

                        skills.nbSkillChoiceLeft = ko.pureComputed(function () {
                            var selected = skills.selectedSkills();
                            var nbSkills = Lazy(selected).where(function (skill) {
                                return !skill.isPassive && !skill.isRacial;
                            }).toArray().length;

                            return data.maxNbSkills - nbSkills;
                        });

                        skills.selectedSkills = ko.observableArray().extend({ notify: 'always' });
                        skills.selectedPassives = ko.observableArray().extend({ notify: 'always' });

                        skills.catAvailable = function (cat) {
                            if (skills.unlockedCats.indexOf(cat) > -1)
                                return true;

                            if(cat.isMastery && Lazy(skills.unlockedCats()).
                                every(function (c) { return c.id !== cat.mastery;
                            })) {
                                return false;
                            }

                            return skills.nbCatChoiceLeft() > 0;
                        }

                        skills.isAvailable = function (skill) {
                            var prereqCond = skill.prereq.length === 0 || Lazy(skill.prereq).some(function (pre) {
                                return pre === Lazy(skills.selectedSkills()).some(function (sel) {
                                    return sel.id === pre;
                                });
                            });

                            var nbSkillsCond = skills.nbSkillChoiceLeft() > 0 || Lazy(skills.selectedSkills()).indexOf(skill) > -1;

                            var catUnlockCond = !!Lazy(skills.unlockedCats()).find(function (unlocked) {
                                return unlocked.catId === skill.categoryId;
                            });

                            return catUnlockCond && prereqCond && nbSkillsCond;
                        };

                        return skills;
                    }

                    self.errors = ko.observableArray();
                    self.formIsValid = function () {
                        var valid = true;
                        self.errors([]);
                        if (self.skills.nbCatChoiceLeft() !== 0) {
                            valid = false;
                            self.errors.push(self.skills.nbCatChoiceLeft() + " choix de catégories restants");
                        }

                        if (self.skills.nbSkillChoiceLeft() !== 0) {
                            valid = false;
                            self.errors.push(self.skills.nbSkillChoiceLeft() + " choix de compétences restants");
                        }

                        if (!self.selectedRace()) {
                            valid = false;
                            self.errors.push("Sélectionnez une race");
                        }

                        if (!self.selectedLanguage() && self.selectedRace.language !== 3) {
                            valid = false;
                            self.errors.push("Sélectionnez une langue supplémentaire");
                        }

                        if (!self.characterName()) {
                            valid = false;
                            self.errors.push("Nommez votre personnage");
                        }
                        return valid;
                    };

                    return self;
                }

                ko.applyBindings(model);
            });
    })