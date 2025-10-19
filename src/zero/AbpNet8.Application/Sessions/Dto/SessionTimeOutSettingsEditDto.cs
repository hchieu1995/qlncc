using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.Sessions.Dto
{
    public class SessionTimeOutSettingsEditDto
    {
        public bool IsEnabled { get; set; }

        [Range(10, int.MaxValue)]
        public int TimeOutSecond { get; set; }

        [Range(10, int.MaxValue)]
        public int ShowTimeOutNotificationSecond { get; set; }
    }
}
