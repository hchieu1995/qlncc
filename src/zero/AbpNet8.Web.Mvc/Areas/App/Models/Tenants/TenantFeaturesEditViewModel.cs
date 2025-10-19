using Abp.AutoMapper;
using AbpNet8.MultiTenancy;
using AbpNet8.MultiTenancy.Dto;
using AbpNet8.Web.Areas.App.Models.Common;

namespace AbpNet8.Web.Areas.App.Models.Tenants
{
    [AutoMapFrom(typeof(GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}