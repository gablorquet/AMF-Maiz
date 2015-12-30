using System.Collections.Generic;

namespace AMF.Core.Model
{
    public class Category : Entity
    {
        public string Name { get; set; }
        
        public List<Skill> Skills { get; set; }

        public bool IsMastery { get; set; }
    }
}