using AbpNet8.Session;
using AbpNet8.Web.Areas.App.Models.Layout;
using AbpNet8.Web.Views;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbpNet8.Web.Areas.Admin.Views.Shared.Components.AppTheme4Footer
{
    public class AppTheme4FooterViewComponent : AbpNet8ViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme4FooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
