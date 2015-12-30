using System.Collections.Generic;
using System.Linq;
using AMF.Core.Model;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public List<SkillViewModel> Skills { get; set; }

        public CategoryViewModel(Category data)
        {
            Name = data.Name;

            Skills = data.Skills.Select(x => new SkillViewModel(x)).ToList();
        }
    }
}
