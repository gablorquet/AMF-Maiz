using System.Collections.Generic;

namespace AMF.Core.Model
{
    public class Legacy : Entity
    {
        public virtual List<LegacyTree> LegacyAvailable { get; set; }

        public virtual List<LegacySkill> LegacySkills { get; set; }

        public Legacy()
        {
            LegacyAvailable = new List<LegacyTree>();
            LegacySkills = new List<LegacySkill>();
        }
    }

    public class LegacyTree : Entity
    {
        public virtual List<LegacySkill> Skills { get; set; }

        public LegacyTree()
        {
            Skills = new List<LegacySkill>();
        }
    }

    public class LegacySkill : Entity
    {
        public int Cost { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<LegacySkill> Prerequisites { get; set; }
    }
}
