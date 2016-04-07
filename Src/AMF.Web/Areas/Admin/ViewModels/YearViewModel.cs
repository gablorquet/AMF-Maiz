using System;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class YearViewModel
    {

        public int YearId { get; set; }

        public string YearName { get; set; }
        public bool Current { get; set; }

        public int? ScenarioId { get; set; }

        public string ScenarioName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}