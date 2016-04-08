require(
    [
        "jquery",
        'knockout',
        "lazy",
        "bootstrap"
    ],
    function ($, ko, Lazy) {

        var model = requireConfig.pageOptions.model;

        $.ajax("/Admin/Character/GetDataForCurrentEvent")
            .done(function (resp) {

                var vm = {};
                vm.Update = function () {
                    var data = resp;

                    var self = this;

                    //Static
                    self.name = model.name;
                    self.id = model.id;
                    self.selectedRace = Lazy(data.races).find(function (race) {
                        return race.id === model.race;
                    });
                    self.lockedSkills = model.skills;
                    self.lockedCats = model.categories;
                    self.lockedLegacies = model.legacies;

                    //Data
                    self.categories = data.cats;
                    self.skills = Lazy(data.cats).pluck('skills').flatten().toArray();
                    self.passives = Lazy(data.cats).pluck('passives').flatten().toArray();
                    self.xp = model.xp;

                    //Selection left 
                    self.maxNbSkills = ko.pureComputed(function () {
                        return data.maxNbSkills - self.selectedSkills().length;
                    });

                    self.maxNbCats = ko.pureComputed(function () {
                        return data.maxNbCats - self.unlockedCats().length;
                    });

                    //Selected
                    self.selectedSkills = ko.observableArray(Lazy(self.skills).where(function (skill) {
                        return Lazy(model.skills).contains(skill.id);
                    }).toArray()).extend({ notify: 'always' });

                    self.unlockedCats = ko.observableArray(Lazy(self.categories).where(function (cat) {
                        return Lazy(model.categories).contains(cat.catId);
                    }).toArray()).extend({ notify: 'always' });
                    self.unlockedCats.subscribe(function (cat) {
                        var unlockedId = Lazy(self.unlockedCats()).pluck('catId').toArray();

                        var allPassives = Lazy(self.passives).where(function (pass) {
                            return Lazy(unlockedId).contains(pass.categoryId);
                        }).toArray();

                        var racials = Lazy(self.selectedRace.skills).where(function (racial) {
                            return !racial.category || Lazy(unlockedId).contains(racial.category);
                        }).toArray();

                        self.racials(racials);
                        self.selectedPassives(allPassives);
                    });

                    self.legaciesAvail = resp.legacies;
                    var legaciesSkills = Lazy(resp.legacies).pluck('skills').flatten().toArray();
                    self.legacies = ko.observableArray(Lazy(legaciesSkills)
                        .where(function (leg) {
                            return Lazy(self.lockedLegacies).contains(leg.id);
                        }).toArray());
                   


                    self.isLegacyAvail = function (legacySkill) {
                        //Is locked
                        if(Lazy(self.lockedLegacies).contains(legacySkill.id))
                            return false;

                        //Has enough XP
                        if (self.xp < (self.currentXPCost()) + legacySkill.cost)
                            return false;

                        //Has Prereq

                        return true;
                    }

                    self.currentXPCost = function() {
                        var newLeg = Lazy(self.legacies).where(function(leg) {
                            return !Lazy(self.lockedLegacies).contains(leg.id);
                        });

                        return Lazy(newLeg).sum('cost');
                    }

                    self.selectedPassives = ko.observableArray(Lazy(self.passives).where(function (pass) {
                        return Lazy(model.passives).contains(pass.id);
                    }).toArray());

                    self.racials = ko.observableArray(Lazy(self.selectedRace.skills).where(function (racial) {
                        var categories = Lazy(self.unlockedCats()).pluck('catId').toArray();

                        return !racial.category || Lazy(categories).contains(racial.categoryId);
                    }).toArray());

                    //Display / Validation
                    self.isAvailable = function (skill) {
                        if (Lazy(self.lockedSkills).contains(skill.id))
                            return false;


                        var isSelected = Lazy(self.selectedSkills()).find(function (sk) {
                            return skill.id === sk.id;
                        });

                        if (isSelected)
                            return true;

                        var catSelected = Lazy(self.unlockedCats()).find(function (cat) {
                            return cat.catId === skill.categoryId;
                        });

                        if (!catSelected)
                            return false;

                        var hasPreReq = Lazy(self.selectedSkills()).some(function (sk) {
                            return Lazy(skill.prereq).contains(sk.id);
                        });

                        if (!hasPreReq)
                            return false;

                        if (self.maxNbSkills() < 1)
                            return false;


                        return true;
                    }

                    self.catAvailable = function (cat) {
                        var isLocked = Lazy(self.lockedCats).contains(cat.catId);

                        if (isLocked)
                            return false;

                        var selected = Lazy(self.unlockedCats()).find(function (category) {
                            return category.catId === cat.catId;
                        });

                        if (selected)
                            return true;

                        if (cat.isMastery && Lazy(skills.unlockedCats())
                            .every(function (c) { return c.id !== cat.mastery; })) {
                            return false;
                        }

                        return self.maxNbCats() > 0;
                    }

                    self.errors = ko.observableArray();
                    self.formIsValid = function () {
                        var valid = true;
                        self.errors([]);
                        if (self.maxNbCats() !== 0) {
                            valid = false;
                            self.errors.push(self.maxNbCats() + " choix de catégories restants");
                        }

                        if (self.maxNbSkills() !== 0) {
                            valid = false;
                            self.errors.push(self.maxNbSkills() + " choix de compétences restants");
                        }

                        return valid;
                    };
                    return self;
                }

                ko.applyBindings(vm);

            });
    })