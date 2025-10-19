using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.Session;
using Abp.Domain.Uow;

namespace AbpNet8.Web.Views.Shared.Components.AccountLogo
{
    public class AccountLogoViewComponent : AbpNet8ViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public AccountLogoViewComponent(IPerRequestSessionCache sessionCache, IUnitOfWorkManager unitOfWorkManager)
        {
            _sessionCache = sessionCache;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string skin)
        {
            using (var ouw = _unitOfWorkManager.Begin())
            {
                var loginInfo = await _sessionCache.GetCurrentLoginInformationsAsync();
                ouw.Complete();
                return View(new AccountLogoViewModel(loginInfo, skin));
            }
        }
    }
}
