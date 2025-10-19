using Abp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8
{
    public abstract class AbpNet8ServiceBase : AbpServiceBase
    {
        protected AbpNet8ServiceBase()
        {
            LocalizationSourceName = AbpNet8Consts.LocalizationSourceName;
        }
    }
}
