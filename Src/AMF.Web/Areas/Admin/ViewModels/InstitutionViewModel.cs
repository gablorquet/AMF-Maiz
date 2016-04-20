using System.Collections.Generic;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class InstitutionViewModel
    {
        public string Name { get; set; }
        public int NatureId { get; set; }

        public List<int> MembersId { get; set; }
        public int LeaderId { get; set; }
    }
}