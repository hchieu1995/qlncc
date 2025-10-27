using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using AbpNet8;
using Admin.Authorization;

namespace AbpNet8.Web.Areas.Admin.Startup
{
    public class AppNavigationProvider : NavigationProvider
    {
        public const string MenuName = "Admin";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));
            menu
                .AddItem(new MenuItemDefinition("HeThong", L("HeThong"), icon: "flaticon-medical", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HeThong))
                            .AddItem(new MenuItemDefinition("HeThong.QuanLyNguoiDung", L("QuanLyNguoiDung"), url: "Admin/QuanLyNguoiDung", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HeThong_NguoiDung)))
                            .AddItem(new MenuItemDefinition("HeThong.QuanLyCoCauToChuc", L("QuanLyCoCauToChuc"), url: "Admin/QuanLyCoCauToChuc", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HeThong_QuanLyCoCauToChuc)))
                            .AddItem(new MenuItemDefinition("HeThong.VaiTro", L("VaiTro"), url: "Admin/QuanLyVaiTro", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HeThong_VaiTro)))
                         )
                .AddItem(new MenuItemDefinition("DanhMuc", L("DanhMuc"), icon: "flaticon-medical", permissionDependency: null)
                        //.AddItem(new MenuItemDefinition("DanhMuc.DanhMucKhac", L("DanhMucKhac"), permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_DanhMuc_Khac))
                        //        .AddItem(new MenuItemDefinition("DanhMuc.DanhMucKhac.TinhThanh", L("DanhMucTinhThanh"), url: "Admin/DanhMucTinhThanh", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_DanhMuc_Khac_TinhThanh)))
                        //        .AddItem(new MenuItemDefinition("DanhMuc.DanhMucKhac.QuanHuyen", L("DanhMucQuanHuyen"), url: "Admin/DanhMucQuanHuyen", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_DanhMuc_Khac_QuanHuyen)))
                        //        )
                        //.AddItem(new MenuItemDefinition("DanhMuc.DonViHanhChinh", L("DonViHanhChinh"), url: "Admin/DonViHanhChinh", permissionDependency: null))
                         )
                ;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpNet8Consts.LocalizationSourceName);
        }
    }
}
