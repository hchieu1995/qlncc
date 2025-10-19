using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using AbpNet8.Authorization;
using Admin.Authorization;

namespace AbpNet8.Web.Areas.App.Startup
{
    public class AppNavigationProvider : NavigationProvider
    {
        public const string MenuName = "App";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            menu
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Host.Dashboard,
                        L("Dashboard"),
                        url: "App/HostDashboard",
                        icon: "flaticon-line-graph"
                    ));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpNet8Consts.LocalizationSourceName);
        }
    }
}