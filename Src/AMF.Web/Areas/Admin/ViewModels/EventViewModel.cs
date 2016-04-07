using System;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class EventViewModel
    {
        public int EventId { get; set; }

        public bool NextEvent { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int EventNumber { get; set; }
        public bool WasCanceled { get; set; }

        
    }
}