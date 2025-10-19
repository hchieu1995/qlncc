using Abp.Dependency;
using Abp.Runtime.Session;
using AbpNet8.MultiTenancy;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpNet8.Web.Views
{
    public class TenantViewLocationExpander : IViewLocationExpander, ITransientDependency
    {
        private string _tenant;

        public TenantViewLocationExpander()
        {
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //if (context.ViewName.StartsWith("Components"))
            //    return viewLocations;
            //return new string[] { "/{0}" + RazorViewEngine.ViewExtension };

            //if (context.ViewName.Contains("Login"))
            //{
            //    return viewLocations;
            //}
            //if (!string.IsNullOrEmpty(_tenant) && _tenant.ToLower() == "phoenix1")
            //{
            //    string[] locations =
            //    {
            //        "/Areas/{2}/Views/{1}/" + _tenant + "/{0}.cshtml",
            //        "/Areas/Views/Common/" + _tenant + "/{0}.cshtml",
            //        "/Areas/{2}/Views/Shared/" + _tenant + "/{0}.cshtml",
            //        //"/Areas/{2}/Views/Shared/{0}.cshtml",
            //        "/Areas/Pages/Shared/" + _tenant + "/{0}.cshtml",
            //        //"/Areas/{2}/Views/{1}/{0}.cshtml",
            //        "/Areas/Views/Common/{0}.cshtml",
            //        "/Views/Shared/{0}.cshtml",
            //        "/Pages/Shared/{0}.cshtml"
            //    };
            //    return locations;
            //}
            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var tenantManager = IocManager.Instance.Resolve<TenantManager>();
            var tenantId = IocManager.Instance.Resolve<IAbpSession>()?.TenantId ?? 0;
            if (tenantId != 0)
            {
                var tenant = tenantManager.GetById(tenantId);
                _tenant = tenant.TenancyName;
            }

            //_tenantService = context.ActionContext.HttpContext.RequestServices.GetRequiredService<ITenantService>();
            //_tenant = _tenantService.GetCurrentTenant();
        }
    }
}
