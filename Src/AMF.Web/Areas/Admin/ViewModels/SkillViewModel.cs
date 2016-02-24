using AMF.Core.Model;
using AMF.Web.Ressources;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class SkillViewModel
    {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }

        public SkillViewModel(Skill data)
        {
            SkillId = data.Id;
            Name = data.Name;
            Category = data.Category != null ? data.Category.Name : Strings.NA;
            CategoryId = data.Category != null ? data.Category.Id : 0;
        }
    }
}