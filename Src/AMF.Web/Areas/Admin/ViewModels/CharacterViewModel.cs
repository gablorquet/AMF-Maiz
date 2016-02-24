using System;
using System.Collections.Generic;
using System.Linq;
using AMF.Core.Enums;
using AMF.Core.Extensions;
using AMF.Core.Model;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class CharacterViewModel
    {

        public List<RaceViewModel> RacesAvailable { get; set; }
        public List<CategoryViewModel> CategoriesAvailable { get; set; }
        public List<SkillViewModel> SkillsAvailable { get; set; } 

        public int NbCategoriesAvailable { get; set; }
        public int NbSkillAvailable { get; set; }
        public int NbLegacyAvailable { get; set; }

        public int SelectedRace { get; set; }
        public List<int> SelectedSkills { get; set; }
        public List<int> SelectedCategories { get; set; }
        public List<int> SelectedLegacySkills { get; set; }

        public List<LanguageViewModel> LanguagesAvailable { get; set; }


        public Player Player { get; set; }
        public string Name { get; set; }

        public CharacterViewModel(Player player, Event currentEvent)
        {
            CategoriesAvailable = currentEvent.Year.PlayableCategories
                .Select(x => new CategoryViewModel(x))
                .ToList();

            RacesAvailable = currentEvent.Year.PlayableRaces
                .Select(x => new RaceViewModel(x))
                .ToList();

            LanguagesAvailable = new List<LanguageViewModel>();
            foreach (Language lng in Enum.GetValues(typeof (Language)))
            {
                LanguagesAvailable.Add(new LanguageViewModel
                {
                    Id = lng, Text = lng.AsDisplayable()
                });
            }

            Player = player;
            var data = new Character();
            NbCategoriesAvailable = data.IsEligibleForNewCategory();
            NbLegacyAvailable = data.IsEligibleForNewLegacy();
            NbSkillAvailable = data.IsEligibleForNewSkill(currentEvent);
        }

        public CharacterViewModel(Character data, Event currentEvent)
        {
            CategoriesAvailable = currentEvent.Year.PlayableCategories
                .Where(x => !data.Categories.Select(y => y.Id).Contains(x.Id))
                .Select(x => new CategoryViewModel(x))
                .ToList();

            RacesAvailable = new List<RaceViewModel>{new RaceViewModel(data.Race)};

            NbCategoriesAvailable = data.IsEligibleForNewCategory();
            NbLegacyAvailable = data.IsEligibleForNewLegacy();
            NbSkillAvailable = data.IsEligibleForNewSkill(currentEvent);

            
            Player = data.Player;
            Name = data.Name;
        }

        public Character AsCharacter()
        {
            return new Character();
        }
    }

    public class LanguageViewModel
    {
        public Language Id { get; set; }
        public string Text { get; set; }
    }
}
