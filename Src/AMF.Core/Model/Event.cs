using System;
using System.Collections.Generic;

namespace AMF.Core.Model
{
    public class Event : Entity
    {
        public DateTime Date { get; set; }

        public DateTime? ClosedDate { get; set; }

        public int EventNumber { get; set; }

        public virtual List<Character> Attendees { get; set; }

        public bool NextEvent { get; set; }

        public bool WasCanceled { get; set; }

        public Year Year { get; set; }

        public int NbOfMaxSkill()
        {
            int byThree = EventNumber/3;

            return Math.Min(byThree +1, 7);
        }
    }
}