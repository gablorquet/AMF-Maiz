using System.Collections.Generic;
using System.Linq;
using AMF.Core.Model;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMastery { get; set; }
        public string ImageUrl { get; set; }

        public List<SkillViewModel> Skills { get; set; }

        public CategoryViewModel(Category data)
        {
            Id = data.Id;
            Name = data.Name;
            IsMastery = data.IsMastery;

            Skills = data.Skills.Select(x => new SkillViewModel(x)).ToList();
        }
    }
}
