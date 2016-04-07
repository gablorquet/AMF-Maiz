using System.Collections.Generic;
using System.Linq;
using AMF.Core.Enums;
using AMF.Core.Model;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class DebriefingViewModel
    {
        public int Gold { get; set; }
        public List<Ressource> Ressources { get; set; }

        public int Minors { get; set; }
        public int Majors { get; set; }

        public int HP { get; set; }

        public int ShortDamage { get; set; }
        public int LongDamage { get; set; }
        public int HeavyDamage { get; set; }

        public bool HeavyArmor { get; set; }

        public DebriefingViewModel(Character character)
        {
            Gold = character.GoldPiecesFromInfluence();
            Ressources = character.RessourceFromInfluence();

            var bonuses = character.Skills.SelectMany(x => x.Bonus).Select(x => x.Bonus).ToList();

            var categories = character.Categories.Select(x => x.Id);

            bonuses.AddRange(character.Race.Skills
                .Where(x => x.Category == null || categories.Contains(x.Category.Id))
                .SelectMany(x => x.Bonus).Select(x=> x.Bonus));

            Minors = bonuses.Count(x => x == Bonus.ThreeMinor)*3 + bonuses.Count(x => x == Bonus.ExtraMinor) ;
            Majors = bonuses.Count(x => x == Bonus.TwoMajor)*3 + bonuses.Count(x => x == Bonus.ExtraMajor);
        }
    }
}