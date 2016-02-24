using System.Collections.Generic;
using AMF.Core.Enums;

namespace AMF.Core.Model
{
    public class Race : Entity
    {
        public string Name { get; set; }

        public virtual List<Skill> Skills { get; set; }

        public virtual Language Language { get; set; }
    }
}