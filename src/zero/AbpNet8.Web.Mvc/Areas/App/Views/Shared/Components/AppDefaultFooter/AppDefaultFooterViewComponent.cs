using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.Web.Areas.App.Models.Layout;
using AbpNet8.Web.Views;
using AbpNet8.Session;
using Abp.Domain.Uow;

namespace AbpNet8.Web.Areas.App.Views.Shared.Components.AppDefaultFooter
{
    public class AppDefaultFooterViewComponent : AbpNet8ViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public AppDefaultFooterViewComponent(IPerRequestSessionCache sessionCache, IUnitOfWorkManager unitOfWorkManager)
        {
            _sessionCache = sessionCache;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            using (var ouw = _unitOfWorkManager.Begin())
            {
                var footerModel = new FooterViewModel
                {
                    LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
                };
                ouw.Complete();

                return View(footerModel);
            }
        }
    }
}
