require(
    [
        "jquery",
        "knockout",
        "lazy"
    ],
    function($, ko, Lazy) {
        var data = requireConfig.pageOptions.model;

        var model = {};
        model.CreateCharacter = function () {
            var self = this;

            self.characterName = ko.observable();
            self.characterName('');

            self.nbSkill = data.NbSkillAvailable;
            self.nbCat = data.NbCategoriesAvailable;
            self.nbLegacy = data.NbLegacyAvailable;

            self.racesAvailable = Lazy(data.RacesAvailable).map(function(race) {
                return {
                    id: race.Id,
                    text: race.Name,
                    details : Lazy(race.Racials).map(function(racial) {
                        return {
                            name: racial.Name
                        }
                    }).toArray()
                }
            })
            .sortBy('text').toArray();

            self.categoriesAvailable = Lazy(data.CategoriesAvailable).map(function(cat) {
                return {
                    id: cat.Id,
                    text: cat.Name,
                    isMastery: cat.IsMastery,
                    imageUrl : cat.ImageUrl
                }
            }).toArray();

            self.selectedRace = ko.observable();
            self.selectedSkills = ko.observableArray([]);
            self.selectedCategories = ko.observableArray([]);
            self.selectedLegacies = ko.observableArray([]);


            self.formIsValid = ko.pureComputed(function() {
                if (self.nbCat !== self.selectedCategories().length)
                    return false;

                if (self.nbSkill !== self.selectedSkills().length)
                    return false;

                return true;
            });

            return self;
        }

        
        ko.applyBindings(model);
    })