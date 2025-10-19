using System.Linq;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.Authorization;
using AbpNet8.Configuration;
using AbpNet8.Web.Areas.App.Models.Layout;
using AbpNet8.Web.Views;
using AbpNet8.Session;
using Abp.Domain.Uow;

namespace AbpNet8.Web.Areas.App.Views.Shared.Components.AppSubscriptionBar
{
    public class AppSubscriptionBarViewComponent : AbpNet8ViewComponent
    {
        private readonly ILanguageManager _languageManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IAbpSession _abpSession;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public AppSubscriptionBarViewComponent(
            IMultiTenancyConfig multiTenancyConfig,
            IAbpSession abpSession,
            ILanguageManager languageManager,
            IPerRequestSessionCache sessionCache, IUnitOfWorkManager unitOfWorkManager)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _abpSession = abpSession;
            _languageManager = languageManager;
            _sessionCache = sessionCache;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            using (var ouw = _unitOfWorkManager.Begin())
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
                ouw.Complete();

                return View(headerModel);
            }
        }

    }
}
