using System.Collections.Generic;
using AMF.Core.Enums;

namespace AMF.Core.Model
{
    public class Institution : Entity
    {
        public string Name { get; set; }

        public Nature Nature { get; set; }

        public virtual List<Character> Characters { get; set; }

        public virtual Character Leader { get; set; }

        public virtual List<LegacySkill> LegaciesUnlocked { get; set; }

        public virtual Year Year { get; set; }
    }

    public class Nature : Entity
    {
        public string Name { get; set; }

        public virtual List<Milestone> Milestones { get; set; }

        public virtual LegacyTree Legacy { get; set; }
    }

    public class Milestone : Entity
    {
        public string Description {get; set; }

        public string Cost { get; set; }
    }
}
