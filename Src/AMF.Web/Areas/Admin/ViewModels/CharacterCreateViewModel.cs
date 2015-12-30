using System.Collections.Generic;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class CharacterCreateViewModel
    {

        public List<RaceViewModel> RacesAvailable { get; set; }
        public List<SkillViewModel> SkillsAvailable { get; set; }
        public List<CategoryViewModel> CategotyAvailable { get; set; }

        public int NbCategoriesAvailable { get; set; }
        public int NbSkillAvailable { get; set; }
        public int NbLegacyAvailable { get; set; }

        public RaceViewModel SelectedRace { get; set; }
        public List<SkillViewModel> SelectedSkills { get; set; }
        public List<CategoryViewModel> SelectedCategories { get; set; }
        public List<SkillViewModel> SelectedLegacySkills { get; set; } 

        public CharacterViewModel Character { get; set; }


        public CharacterCreateViewModel()
        {
            
        }
    }
}
