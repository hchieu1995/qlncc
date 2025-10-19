using AbpNet8.Sessions.Dto;
using Abp.Extensions;

namespace AbpNet8.Web.Areas.App.Models.Layout
{
    public class LogoViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public string LogoSkinOverride { get; set; }

        public string LogoClassOverride { get; set; }
        public string LogoUrl { get; set; }

        public string GetLogoUrl(string appPath, string logoSkin)
        {
            if (!LogoSkinOverride.IsNullOrEmpty())
            {
                logoSkin = LogoSkinOverride;
            }

            if (LoginInformations?.Tenant?.LogoId == null)
            {
                return appPath + $"Common/Images/logohopdong.jpg";
            }

            //id parameter is used to prevent caching only.
            return appPath + "TenantCustomization/GetLogo?tenantId=" + LoginInformations?.Tenant?.Id;
        }
    }
}
