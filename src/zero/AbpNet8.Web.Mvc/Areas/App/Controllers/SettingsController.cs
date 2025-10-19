//using System.Threading.Tasks;
//using Abp.AspNetCore.Mvc.Authorization;
//using Abp.Configuration;
//using Abp.Configuration.Startup;
//using Abp.Runtime.Session;
//using Abp.Timing;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.Authorization.Users;
//using AbpNet8.Configuration.Tenants;
//using AbpNet8.MultiTenancy;
//using AbpNet8.Timing;
//using AbpNet8.Timing.Dto;
//using AbpNet8.Web.Areas.App.Models.Settings;
//using AbpNet8.Web.Controllers;
//using HopDong.Core.Authorization;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    [AbpMvcAuthorize(AppPermissions.Pages)]
//    public class SettingsController : AbpNet8ControllerBase
//    {
//        private readonly UserManager _userManager;
//        private readonly TenantManager _tenantManager;
//        private readonly ITenantSettingsAppService _tenantSettingsAppService;
//        private readonly IMultiTenancyConfig _multiTenancyConfig;
//        private readonly ITimingAppService _timingAppService;

//        public SettingsController(
//            ITenantSettingsAppService tenantSettingsAppService,
//            IMultiTenancyConfig multiTenancyConfig,
//            ITimingAppService timingAppService, 
//            UserManager userManager, 
//            TenantManager tenantManager)
//        {
//            _tenantSettingsAppService = tenantSettingsAppService;
//            _multiTenancyConfig = multiTenancyConfig;
//            _timingAppService = timingAppService;
//            _userManager = userManager;
//            _tenantManager = tenantManager;
//        }

//        public async Task<ActionResult> Index()
//        {
//            var output = await _tenantSettingsAppService.GetAllSettings();
//            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;

//            var timezoneItems = await _timingAppService.GetTimezoneComboboxItems(new GetTimezoneComboboxItemsInput
//            {
//                DefaultTimezoneScope = SettingScopes.Tenant,
//                SelectedTimezoneId = await SettingManager.GetSettingValueForTenantAsync(TimingSettingNames.TimeZone, AbpSession.GetTenantId())
//            });

//            var user = await _userManager.GetUserAsync(AbpSession.ToUserIdentifier());

//            ViewBag.CurrentUserEmail = user.EmailAddress;

//            var tenant = await _tenantManager.FindByIdAsync(AbpSession.GetTenantId());
//            ViewBag.TenantId = tenant.Id;
//            ViewBag.TenantLogoId = tenant.LogoId;
//            ViewBag.TenantCustomCssId = tenant.CustomCssId;

//            var model = new SettingsViewModel
//            {
//                Settings = output,
//                TimezoneItems = timezoneItems
//            };

//            return View(model);
//        }
//    }
//}