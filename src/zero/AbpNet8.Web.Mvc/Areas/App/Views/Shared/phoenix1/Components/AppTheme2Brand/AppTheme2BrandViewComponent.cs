using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.Web.Areas.App.Models.Layout;
using AbpNet8.Session;
using AbpNet8.Web.Views;
using Abp.Domain.Uow;

namespace AbpNet8.Web.Areas.App.Views.Shared.phoenix1.Components.AppTheme2Brand
{
    public class AppTheme2BrandViewComponent : AbpNet8ViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public AppTheme2BrandViewComponent(IPerRequestSessionCache sessionCache, IUnitOfWorkManager unitOfWorkManager)
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
