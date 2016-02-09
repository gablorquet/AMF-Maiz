using System.Collections.Generic;

namespace AMF.Core.Model
{
    public class Race : Entity
    {
        public string Name { get; set; }

        public virtual List<Skill> Skills { get; set; }
    }
}