using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using AbpNet8.Configuration;
using AbpNet8.Session;
using AbpNet8.Web.Areas.App.Models.Layout;
using AbpNet8.Web.Views;
using Admin.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AbpNet8.Web.Areas.Admin.Views.Shared.Components.AppSubscriptionBar
{
    public class AppSubscriptionBarViewComponent : AbpNet8ViewComponent
    {
        private readonly ILanguageManager _languageManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IAbpSession _abpSession;

        public AppSubscriptionBarViewComponent(
            IMultiTenancyConfig multiTenancyConfig,
            IAbpSession abpSession,
            ILanguageManager languageManager,
            IPerRequestSessionCache sessionCache)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _abpSession = abpSession;
            _languageManager = languageManager;
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync(),
                Languages = _languageManager.GetActiveLanguages().ToList(),
                CurrentLanguage = _languageManager.CurrentLanguage,
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                IsImpersonatedLogin = _abpSession.ImpersonatorUserId.HasValue,
                HasUiCustomizationPagePermission = true,
                SubscriptionExpireNootifyDayCount = SettingManager.GetSettingValue<int>(AppSettings.TenantManagement.SubscriptionExpireNotifyDayCount)
            };

            return View(headerModel);
        }

    }
}
