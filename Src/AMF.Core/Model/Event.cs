using System;
using System.Collections.Generic;

namespace AMF.Core.Model
{
    public class Event : Entity
    {
        public DateTime Date { get; set; }

        public int EventNumber { get; set; }

        public List<Character> Attendees { get; set; }

        public Year Year { get; set; }


        public int NbOfMaxSkill()
        {
            return Math.Max(EventNumber/3 + 1, 7);
        }
    }
}