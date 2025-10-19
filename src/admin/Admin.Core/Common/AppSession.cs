using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Common
{
    public class AppSession : ClaimsAbpSession, ITransientDependency
    {
        public AppSession(IPrincipalAccessor principalAccessor,
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
            : base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {

        }

        public string User_MaDonVi
        {
            get
            {
                var userDnMstClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "MaDonVi");
                if (string.IsNullOrEmpty(userDnMstClaim?.Value))
                {
                    return null;
                }

                return userDnMstClaim.Value;
            }
        }

        public string User_TenDonVi
        {
            get
            {
                var userDnMstClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "Application_TenDonVi");
                if (string.IsNullOrEmpty(userDnMstClaim?.Value))
                {
                    return null;
                }

                return userDnMstClaim.Value;
            }
        }
        
    }
}
