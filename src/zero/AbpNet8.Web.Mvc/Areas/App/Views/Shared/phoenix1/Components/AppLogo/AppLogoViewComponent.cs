using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.Web.Areas.App.Models.Layout;
using AbpNet8.Session;
using AbpNet8.Web.Views;
using Abp.Domain.Uow;

namespace AbpNet8.Web.Areas.App.Views.Shared.phoenix1.Components.AppLogo
{
    public class AppLogoViewComponent : AbpNet8ViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public AppLogoViewComponent(
            IPerRequestSessionCache sessionCache, IUnitOfWorkManager unitOfWorkManager
        )
        {
            _sessionCache = sessionCache;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string logoSkin = null, string logoClass = "")
        {
            using (var ouw = _unitOfWorkManager.Begin())
            {
                var headerModel = new LogoViewModel
                {
                    LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync(),
                    LogoSkinOverride = logoSkin,
                    LogoClassOverride = logoClass
                };
                ouw.Complete();

                return View(headerModel);
            }
        }
    }
}
