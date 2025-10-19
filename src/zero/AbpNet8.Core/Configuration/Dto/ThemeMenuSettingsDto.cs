using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.Configuration.Dto
{
    public class ThemeMenuSettingsDto
    {
        public string Position { get; set; }

        public string AsideSkin { get; set; }

        public bool FixedAside { get; set; }

        public bool AllowAsideMinimizing { get; set; }

        public bool DefaultMinimizedAside { get; set; }

        public string SubmenuToggle { get; set; }

        public bool SearchActive { get; set; }
    }
}
