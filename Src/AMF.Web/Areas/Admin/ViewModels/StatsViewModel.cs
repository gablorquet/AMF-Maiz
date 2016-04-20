using System.Collections.Generic;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class StatsViewModel
    {
        public int eventNumber { get; set; }
        public int attendees { get; set; }
        public double ageAvg { get; set; }
        public List<CatStatsViewModel> categories { get; set; }
    }

    public class CatStatsViewModel
    {
        public string name { get; set; }
        public int count { get; set; }
        public int percentage { get; set; }
    }
}