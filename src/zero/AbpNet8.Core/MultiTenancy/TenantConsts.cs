using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.MultiTenancy
{
    public class TenantConsts
    {
        public const string TenancyNameRegex = "^[a-zA-Z][a-zA-Z0-9_-]{1,}$";

        public const string DefaultTenantName = "Default";

        public const int MaxNameLength = 128;

        public const int DefaultTenantId = 1;
    }
}
