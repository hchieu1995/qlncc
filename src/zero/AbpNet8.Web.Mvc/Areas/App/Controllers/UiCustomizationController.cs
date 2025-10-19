//using System.Threading.Tasks;
//using Abp.AspNetCore.Mvc.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.Authorization;
//using AbpNet8.Configuration;
//using AbpNet8.Web.Areas.App.Models.UiCustomization;
//using AbpNet8.Web.Controllers;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    [AbpMvcAuthorize]
//    public class UiCustomizationController : AbpNet8ControllerBase
//    {
//        private readonly IUiCustomizationSettingsAppService _uiCustomizationAppService;

//        public UiCustomizationController(IUiCustomizationSettingsAppService uiCustomizationAppService)
//        {
//            _uiCustomizationAppService = uiCustomizationAppService;
//        }

//        public async Task<ActionResult> Index()
//        {
//            var model = new UiCustomizationViewModel
//            {
//                Theme = await SettingManager.GetSettingValueAsync(AppSettings.UiManagement.Theme),
//                //Theme = "theme4",
//                Settings = await _uiCustomizationAppService.GetUiManagementSettings(),
//                HasUiCustomizationPagePermission = true
//            };

//            return View(model);
//        }
//    }
//}