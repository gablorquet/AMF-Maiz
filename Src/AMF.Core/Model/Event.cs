using System;
using System.Collections.Generic;

namespace AMF.Core.Model
{
    public class Event : Entity
    {
        public DateTime Date { get; set; }

        public int EventNumber { get; set; }

        public virtual List<Character> Attendees { get; set; }

        public virtual Year Year { get; set; }

        public bool NextEvent { get; set; }

        public int NbOfMaxSkill()
        {
            return Math.Max(EventNumber/3 + 1, 7);
        }
    }
}