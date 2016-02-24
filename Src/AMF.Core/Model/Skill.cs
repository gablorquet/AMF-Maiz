using System.Collections.Generic;
using AMF.Core.Enums;

namespace AMF.Core.Model
{
    public class Skill : Entity
    {
        public string Name { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Skill> Prerequisites { get; set; }

        public virtual List<Spell> Spells { get; set; }

        public virtual List<Bonus> Bonus { get; set; }

        public bool IsLegacy { get; set; }
        public bool IsPassive { get; set; }
        public bool IsRacial { get; set; }

        public bool ArmorRestricted { get; set; }
    }
}