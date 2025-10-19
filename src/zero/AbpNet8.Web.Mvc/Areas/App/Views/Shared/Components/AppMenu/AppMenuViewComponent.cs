using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.MultiTenancy;
using AbpNet8.Web.Areas.App.Models.Layout;
using AbpNet8.Web.Views;
using AbpNet8.Web.Areas.App.Startup;
using Abp.Domain.Uow;

namespace AbpNet8.Web.Areas.App.Views.Shared.Components.AppMenu
{
    public class AppMenuViewComponent : AbpNet8ViewComponent
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IAbpSession _abpSession;
        private readonly TenantManager _tenantManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public AppMenuViewComponent(
            IUserNavigationManager userNavigationManager,
            IAbpSession abpSession,
            TenantManager tenantManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _userNavigationManager = userNavigationManager;
            _abpSession = abpSession;
            _tenantManager = tenantManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isLeftMenuUsed, string currentPageName = null)
        {
            using (var ouw = _unitOfWorkManager.Begin())
            {
                var model = new MenuViewModel
                {
                    Menu = await _userNavigationManager.GetMenuAsync(AppNavigationProvider.MenuName, _abpSession.ToUserIdentifier()),
                    CurrentPageName = currentPageName
                };

                if (AbpSession.TenantId == null)
                {
                    ouw.Complete();
                    return GetView(model, isLeftMenuUsed);
                }

                var tenant = await _tenantManager.GetByIdAsync(AbpSession.TenantId.Value);
                if (tenant.EditionId.HasValue)
                {
                    ouw.Complete();
                    return GetView(model, isLeftMenuUsed);
                }

                var subscriptionManagement = FindMenuItemOrNull(model.Menu.Items, AppPageNames.Tenant.SubscriptionManagement);
                if (subscriptionManagement != null)
                {
                    subscriptionManagement.IsVisible = false;
                }
                ouw.Complete();

                return GetView(model, isLeftMenuUsed);
            }
        }

        public UserMenuItem FindMenuItemOrNull(IList<UserMenuItem> userMenuItems, string name)
        {
            if (userMenuItems == null)
            {
                return null;
            }

            foreach (var menuItem in userMenuItems)
            {
                if (menuItem.Name == name)
                {
                    return menuItem;
                }

                var found = FindMenuItemOrNull(menuItem.Items, name);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        private IViewComponentResult GetView(MenuViewModel model, bool isLeftMenuUsed)
        {
            return View(isLeftMenuUsed ? "Default" : "Top", model);
        }
    }
}
