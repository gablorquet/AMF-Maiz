using System.Collections.Generic;

namespace AMF.Core.Model
{
    public class Year : Entity
    {
        public string Name { get; set; }

        public bool Current { get; set; }

        public Scenario Scenario { get; set; }

        public virtual List<Event> Events { get; set; } 

        public virtual List<Race> PlayableRaces { get; set; }
        public virtual List<Category> PlayableCategories { get; set; }


        public Year()
        {
            Events = new List<Event>();
            PlayableRaces = new List<Race>();
            PlayableCategories = new List<Category>();
        }
    }

    public class Scenario : Entity
    {
        public virtual List<Year> Years { get; set; } 

    }
}
