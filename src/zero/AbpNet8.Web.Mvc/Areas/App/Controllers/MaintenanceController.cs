//using Abp.AspNetCore.Mvc.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.Caching;
//using AbpNet8.Web.Areas.App.Models.Maintenance;
//using AbpNet8.Web.Controllers;
//using HopDong.Core.Authorization;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    //[AbpMvcAuthorize(AppPermissions.Pages)]
//    public class MaintenanceController : AbpNet8ControllerBase
//    {
//        private readonly ICachingAppService _cachingAppService;

//        public MaintenanceController(ICachingAppService cachingAppService)
//        {
//            _cachingAppService = cachingAppService;
//        }

//        public ActionResult Index()
//        {
//            var model = new MaintenanceViewModel
//            {
//                Caches = _cachingAppService.GetAllCaches().Items
//            };

//            return View(model);
//        }
//    }
//}