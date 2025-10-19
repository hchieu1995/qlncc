using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.Session;
using Abp.Domain.Uow;

namespace AbpNet8.Web.Views.Shared.Components.TenantChange
{
    public class TenantChangeViewComponent : AbpNet8ViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TenantChangeViewComponent(IPerRequestSessionCache sessionCache,IUnitOfWorkManager unitOfWorkManager)
        {
            _sessionCache = sessionCache; 
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            using (var ouw = _unitOfWorkManager.Begin())
            {
                var loginInfo = await _sessionCache.GetCurrentLoginInformationsAsync();
                var model = ObjectMapper.Map<TenantChangeViewModel>(loginInfo); 
                ouw.Complete();
                return View(model);
            }
        }
    }
}
