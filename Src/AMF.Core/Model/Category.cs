using System.Collections.Generic;

namespace AMF.Core.Model
{
    public class Category : Entity
    {
        public string Name { get; set; }
        
        public virtual List<Skill> Skills { get; set; }

        public virtual Category Mastery { get; set; }

        public virtual List<LegacyTree> Legacies { get; set; }

        public Category()
        {
            Skills = new List<Skill>();
            Legacies = new List<LegacyTree>();
        }
    }
}