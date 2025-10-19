using Abp.AspNetCore.Mvc.Authorization;
using AbpNet8.Controllers;
using AbpNet8.Session;
using AbpNet8.Web.Areas.App.Models.Editions;
using Admin.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbpNet8.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages)]
    public class SubscriptionManagementController : AbpNet8ControllerBase
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public SubscriptionManagementController(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<ActionResult> Index()
        {
            var loginInfo = await _sessionCache.GetCurrentLoginInformationsAsync();
            var model = new SubscriptionDashboardViewModel
            {
                LoginInformations = loginInfo
            };

            return View(model);
        }
    }
}