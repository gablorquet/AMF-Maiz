using System.Collections.Generic;

namespace AMF.Core.Model
{
    public class Legacy : Entity
    {
        public List<LegacyTree> LegacyAvailable { get; set; }

        public List<LegacySkill> LegacySkills { get; set; }

        public Scenario Scenario { get; set; }

        public void Prune()
        {
        }
    }

    public class LegacyTree : Entity
    {
        public List<LegacySkill> Skills { get; set; }
    }

    public class LegacySkill : Entity
    {
        
    }
}
