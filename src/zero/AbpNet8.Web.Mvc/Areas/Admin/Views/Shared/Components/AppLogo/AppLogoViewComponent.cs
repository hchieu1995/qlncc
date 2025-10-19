using AbpNet8.Session;
using AbpNet8.Web.Areas.App.Models.Layout;
using AbpNet8.Web.Views;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbpNet8.Web.Areas.Admin.Views.Shared.Components.AppLogo
{
    public class AppLogoViewComponent : AbpNet8ViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppLogoViewComponent(
            IPerRequestSessionCache sessionCache
        )
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync(string logoSkin = null, string logoClass = "")
        {

            var headerModel = new LogoViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync(),
                LogoSkinOverride = logoSkin,
                LogoClassOverride = logoClass
            };

            return View(headerModel);
        }
    }
}
