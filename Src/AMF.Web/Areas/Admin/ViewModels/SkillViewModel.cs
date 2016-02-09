using AMF.Core.Model;
using AMF.Web.Ressources;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class SkillViewModel
    {

        public string Name { get; set; }
        public string Category { get; set; }

        public SkillViewModel(Skill data)
        {
            Name = data.Name;
            Category = data.Category != null ? data.Category.Name : Strings.NA;
        }
    }
}