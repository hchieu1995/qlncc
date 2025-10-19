using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.Configuration.Dto
{
    public class ThemeHeaderSettingsDto
    {
        public bool DesktopFixedHeader { get; set; }

        public bool MobileFixedHeader { get; set; }

        public string HeaderSkin { get; set; }

        public string MinimizeDesktopHeaderType { get; set; }

        public bool HeaderMenuArrows { get; set; }
    }
}
