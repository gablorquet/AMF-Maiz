using System.Collections.Generic;
using System.Linq;
using AMF.Core.Enums;

namespace AMF.Core.Model
{
    public class Character : Entity
    {
        public virtual Player Player { get; set; }

        public string Name { get; set; }
        
        public virtual List<Category> Categories { get; set; }

        public virtual List<Skill> Skills { get; set; }

        public virtual List<Spell> Spells { get; set; }

        public virtual Race Race { get; set; }

        public virtual Year Year { get; set; }

        public virtual List<Event> Presences { get; set; }

        public int Experience { get; set; }

        public string Title { get; set; }

        public virtual Legacy Legacy { get; set; }

        public virtual List<Throphy> Throphies { get; set; }

        public virtual Institution Institution { get; set; }

        public int Influence { get; set; }
        public Ressource? PresetRessource { get; set; }

        public Character()
        {
            Categories = new List<Category>();
            Skills = new List<Skill>();
            Spells = new List<Spell>();
            Throphies = new List<Throphy>();
        }

        public int GoldPiecesFromInfluence()
        {
            var racial = 0;
            if (Race.Skills.Any(x => x.Bonus.Select(y => y.Bonus).Contains(Bonus.ExtraGoldFromInfluence)))
                racial++;

            if (Influence >= 3)
                return racial + 1;
            if (Influence >= 12)
                return racial + 2;
            if (Influence >= 32)
                return racial + 4;
            if (Influence >= 52)
                return racial + 6;
            if (Influence >= 76)
                return racial + 8;

            return 0;
        }

        public List<Ressource> RessourceFromInfluence()
        {
            var rewards = new List<Ressource>();
            if (PresetRessource.HasValue)
                rewards.Add(PresetRessource.Value);

            if(Influence >= 76)
                rewards.Add(Ressource.Any);

            return rewards;
        }

        public void UpdateFrom(Character character)
        {
            
        }
    }

    public class Throphy : Entity
    {
        public Character Recipient { get; set; }
        public Reward RewardSelected { get;set; }
    }
}