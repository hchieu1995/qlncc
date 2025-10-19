using AbpNet8.Session;
using AbpNet8.Web.Areas.App.Models.Layout;
using AbpNet8.Web.Views;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbpNet8.Web.Areas.Admin.Views.Shared.Components.AppTheme4Brand
{
    public class AppTheme4BrandViewComponent : AbpNet8ViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme4BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
