using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AMF.Core.Enums;

namespace AMF.Core.Model
{
    public class Skill : Entity
    {
        public string Name { get; set; }

        [NotMapped]
        public virtual Category Category { get; set; }

        public Race Race { get; set; }

        public virtual List<Skill> Prerequisites { get; set; }

        public virtual List<Spell> Spells { get; set; }

        public virtual List<SkillBonus> Bonus { get; set; }

        public bool IsLegacy { get; set; }
        public bool IsPassive { get; set; }
        public bool IsRacial { get; set; }

        public bool ArmorRestricted { get; set; }

        public virtual List<Character> Characters { get; set; } 

        public Skill()
        {
            Prerequisites = new List<Skill>();
            Spells = new List<Spell>();
            Bonus = new List<SkillBonus>();
        }
    }

    public class SkillBonus : Entity
    {
        public Bonus Bonus { get; set; }
    }
}