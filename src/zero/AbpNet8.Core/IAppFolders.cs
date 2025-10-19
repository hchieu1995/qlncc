using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8
{
    public interface IAppFolders
    {
        string TempFileDownloadFolder { get; }
        string SampleProfileImagesFolder { get; }

        string WebLogsFolder { get; set; }
    }
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string SampleProfileImagesFolder { get; set; }

        public string WebLogsFolder { get; set; }

        public string TempFileDownloadFolder { get; set; }
    }
}
