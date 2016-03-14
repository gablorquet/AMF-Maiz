using System.Collections.Generic;
using System.Linq;
using AMF.Core.Model;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class CharacterViewModel
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int CharacterId { get; set; }
        public string CharacterName { get; set; }

        public int SelectedRace { get; set; }

        public List<int> SelectedSkills { get; set; }

        public List<int> SelectedCategories { get; set; }

        public int NbPresences { get; set; }

        public CharacterViewModel()
        {
            
        }

        public CharacterViewModel(Character character)
        {
            PlayerId = character.Player.Id;
            Player = character.Player;
            CharacterName = character.Name;
            SelectedRace = character.Race.Id;
            SelectedSkills = character.Skills.Select(x => x.Id).ToList();
            SelectedCategories = character.Categories.Select(x => x.Id).ToList();
            CharacterId = character.Id;
            NbPresences = character.Presences.Count;
        }

    }
}