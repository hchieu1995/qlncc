using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using AbpNet8.Sessions.Dto;

namespace AbpNet8.Web.Areas.App.Models.Layout
{
    public class HeaderViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }

        public LanguageInfo CurrentLanguage { get; set; }

        public bool IsMultiTenancyEnabled { get; set; }

        public bool IsImpersonatedLogin { get; set; }

        public bool HasUiCustomizationPagePermission { get; set; }

        public int SubscriptionExpireNootifyDayCount { get; set; }

        public string ProfileImage { get; set; }
        public string GetShownLoginName()
        {
            var userName = "<a href=\"#\" class=\"idUserLogin\"  style=\"color:white\"><span id=\"HeaderCurrentUserName\">" + LoginInformations.User?.UserName + "</span></a>";

            if (!IsMultiTenancyEnabled)
            {
                return userName;
            }

            return LoginInformations.Tenant == null
                ? "<span class='tenancy-name'>.\\</span>" + userName
                : "<span class='tenancy-name'>" + LoginInformations.Tenant.TenancyName + "\\" + "</span>" + userName;
        }

        public string GetLogoUrl(string appPath, string logoSkin)
        {
            if (LoginInformations?.Tenant?.LogoId == null)
            {
                return appPath + $"Common/Images/logohopdong.jpg";
            }

            //id parameter is used to prevent caching only.
            return appPath + "TenantCustomization/GetLogo?tenantId=" + LoginInformations?.Tenant?.Id;
        }
        public string VerSion { get; set; }
    }
}