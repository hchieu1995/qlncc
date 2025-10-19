//using Abp.AspNetCore.Mvc.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.Authorization;
//using AbpNet8.DashboardCustomization;
//using AbpNet8.Web.DashboardCustomization;
//using System.Threading.Tasks;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    public class HostDashboardController : CustomizableDashboardControllerBase
//    {
//        public HostDashboardController(
//            DashboardViewConfiguration dashboardViewConfiguration,
//            IDashboardCustomizationAppService dashboardCustomizationAppService)
//            : base(dashboardViewConfiguration, dashboardCustomizationAppService)
//        {

//        }

//        public async Task<ActionResult> Index()
//        {
//            return await GetView(AbpNet8DashboardCustomizationConsts.DashboardNames.DefaultHostDashboard);
//        }
//    }
//}