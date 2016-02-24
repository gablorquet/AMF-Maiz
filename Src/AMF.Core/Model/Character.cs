using System;
using System.Collections.Generic;
using System.Linq;
using AMF.Core.Enums;

namespace AMF.Core.Model
{
    public class Character : Entity
    {
        public Player Player { get; set; }

        public string Name { get; set; }
        
        public List<Category> Categories { get; set; }

        public List<Skill> Skills { get; set; }

        public List<Spell> Spells { get; set; }

        public Race Race { get; set; }

        public Year Year { get; set; }

        public List<Event> Presences { get; set; }


        public int Experience { get; set; }

        public string Title { get; set; }

        public Legacy Legacy { get; set; }

        public List<Throphy> Throphies { get; set; }


        public int Influence { get; set; }
        public Ressource? PresetRessource { get; set; }

        public Character()
        {
            Categories = new List<Category>();
            Skills = new List<Skill>();
            Spells = new List<Spell>();
            Throphies = new List<Throphy>();
        }

        public int IsEligibleForNewCategory()
        {
            if (Categories.Count == 0)
                return 1;

            var nbPresence = Presences.Count + 1;

            return Math.Max((nbPresence / 5) - Categories.Count, 3);
        }

        public int IsEligibleForNewSkill(Event currentEvent)
        {
            if (currentEvent.EventNumber == 0)
                return 1;

            return currentEvent.NbOfMaxSkill() - Skills.Count;
        }

        public int IsEligibleForNewLegacy()
        {
            return Experience / 5;
        }

        public int GoldPiecesFromInfluence()
        {
            var racial = 0;
            if (Race.Skills.Any(x => x.Bonus.Contains(Bonus.ExtraGoldFromInfluence)))
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