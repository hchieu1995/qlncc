//using Abp.AspNetCore.Mvc.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.DashboardCustomization;
//using AbpNet8.Web.DashboardCustomization;
//using System.Threading.Tasks;
//using HopDong.Core.Authorization;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    [AbpMvcAuthorize(AppPermissions.Pages)]
//    public class TenantDashboardController : CustomizableDashboardControllerBase
//    {
//        public TenantDashboardController(DashboardViewConfiguration dashboardViewConfiguration, 
//            IDashboardCustomizationAppService dashboardCustomizationAppService) 
//            : base(dashboardViewConfiguration, dashboardCustomizationAppService)
//        {

//        }

//        public async Task<ActionResult> Index()
//        {
//            return await GetView(AbpNet8DashboardCustomizationConsts.DashboardNames.DefaultTenantDashboard);
//        }
//    }
//}