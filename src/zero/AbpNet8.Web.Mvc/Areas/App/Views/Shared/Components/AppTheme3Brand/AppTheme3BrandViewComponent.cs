using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.Web.Areas.App.Models.Layout;
using AbpNet8.Session;
using AbpNet8.Web.Views;
using Abp.Domain.Uow;

namespace AbpNet8.Web.Areas.App.Views.Shared.Components.AppTheme3Brand
{
    public class AppTheme3BrandViewComponent : AbpNet8ViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public AppTheme3BrandViewComponent(IPerRequestSessionCache sessionCache, IUnitOfWorkManager unitOfWorkManager)
        {
            _sessionCache = sessionCache;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            using (var ouw = _unitOfWorkManager.Begin())
            {
                var headerModel = new HeaderViewModel
                {
                    LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
                };
                ouw.Complete();

                return View(headerModel);
            }
        }
    }
}
