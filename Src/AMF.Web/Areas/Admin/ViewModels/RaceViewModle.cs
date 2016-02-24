using System.Collections.Generic;
using System.Linq;
using AMF.Core.Extensions;
using AMF.Core.Model;

namespace AMF.Web.Areas.Admin.ViewModels
{
    public class RaceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<SkillViewModel> Racials { get; set; }
        public LanguageViewModel Language { get; set; } 

        public RaceViewModel(Race data)
        {
            Id = data.Id;
            Name = data.Name;

            Racials = data.Skills.Select(x => new SkillViewModel(x)).ToList();
            Language = new LanguageViewModel
            {
                Id = data.Language,
                Text = data.Language.AsDisplayable()
            };
        }

    }
}
