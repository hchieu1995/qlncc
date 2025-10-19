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
                .AddItem(new MenuItemDefinition("HeThong", L("HeThong"), icon: "flaticon-medical", permissionDependency: null)
                            .AddItem(new MenuItemDefinition("HeThong.QuanLyNguoiDung", L("QuanLyNguoiDung"), url: "Admin/QuanLyNguoiDung", permissionDependency: null))
                            .AddItem(new MenuItemDefinition("HeThong.VaiTro", L("VaiTro"), url: "Admin/QuanLyVaiTro", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HeThong_VaiTro)))
                            .AddItem(new MenuItemDefinition("HeThong.ThongTinDoanhNghiep", L("ThongTinDoanhNghiep"), url: "Admin/QuanLyDoanhNghiep/CauHinhTongHop", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep)))
                         )
                .AddItem(new MenuItemDefinition("DanhMuc", L("DanhMuc"), icon: "flaticon-medical", permissionDependency: null)
                        //.AddItem(new MenuItemDefinition("DanhMuc.DanhMucKhac", L("DanhMucKhac"), permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_DanhMuc_Khac))
                        //        .AddItem(new MenuItemDefinition("DanhMuc.DanhMucKhac.TinhThanh", L("DanhMucTinhThanh"), url: "Admin/DanhMucTinhThanh", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_DanhMuc_Khac_TinhThanh)))
                        //        .AddItem(new MenuItemDefinition("DanhMuc.DanhMucKhac.QuanHuyen", L("DanhMucQuanHuyen"), url: "Admin/DanhMucQuanHuyen", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_DanhMuc_Khac_QuanHuyen)))
                        //        )
                        .AddItem(new MenuItemDefinition("DanhMuc.DonViHanhChinh", L("DonViHanhChinh"), url: "Admin/DonViHanhChinh", permissionDependency: null))
                         )
                .AddItem(new MenuItemDefinition("DoanhNghiep", L("DoanhNghiep"), icon: "flaticon-medical", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HeThong))
                            .AddItem(new MenuItemDefinition("DoanhNghiep.QuanLyDoanhNghiep", L("QuanLyDoanhNghiep"), url: "Admin/QuanLyDoanhNghiep", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_DanhSachDoanhNghiep)))
                         )
                .AddItem(new MenuItemDefinition("HoaDon", L("HoaDon"), icon: "flaticon-medical")
                            .AddItem(new MenuItemDefinition("HoaDon.DauVao", L("HoaDonDauVao"), url: "Admin/HoaDonDauVao", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HoaDon_DauVao)))
                            .AddItem(new MenuItemDefinition("HoaDon.DauRa", L("HoaDonDauRa"), url: "Admin/HoaDonDauRa", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HoaDon_DauRa)))
                            .AddItem(new MenuItemDefinition("HoaDon.LichSuDongBo", L("DongBoLichSu"), url: "Admin/DongBoLichSu", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HoaDon_LichSuDongBo)))
                            .AddItem(new MenuItemDefinition("HoaDon.QuanLyTaiLieu", L("QuanLyTaiLieu"), url: "Admin/QuanLyTaiLieu", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HoaDon_QuanLyTaiLieu)))
                        )
                .AddItem(new MenuItemDefinition("HopThu", L("HopThu"), icon: "flaticon-medical")
                            .AddItem(new MenuItemDefinition("HopThu.DanhSachMail", L("HopThu"), url: "Admin/QuanLyHopThu", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HopThu)))
                            .AddItem(new MenuItemDefinition("HopThu.LichSuDocMail", L("Mail_LichSu"), url: "Admin/LichSuDocMail", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_HopThu_LichSuDocMail)))
                        )
                .AddItem(new MenuItemDefinition("BaoCao", L("BaoCao"), icon: "flaticon-medical")
                            .AddItem(new MenuItemDefinition("BaoCao.TrangThaiHoaDon", L("TrangThaiHoaDon"), url: "Admin/BaoCaoTrangThaiHoaDon", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_BaoCao_TrangThaiHoaDon)))
                            .AddItem(new MenuItemDefinition("BaoCao.ToKhaiThueGTGT", L("ToKhaiThueGTGT"), url: "Admin/BaoCaoToKhaiThueGTGT", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_BaoCao_ToKhaiThueGTGT)))
                            .AddItem(new MenuItemDefinition("BaoCao.ToKhaiGiamThue", L("ToKhaiGiamThue"), url: "Admin/BaoCaoToKhaiGiamThue", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_BaoCao_ToKhaiGiamThue)))
                            .AddItem(new MenuItemDefinition("BaoCao.BangKeHoaDon", L("BangKeHoaDon"), url: "Admin/BangKeHoaDon", permissionDependency: new SimplePermissionDependency(AppPermissions.Admin_BaoCao_BangKeHoaDon)))
                        )
                ;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpNet8Consts.LocalizationSourceName);
        }
    }
}
