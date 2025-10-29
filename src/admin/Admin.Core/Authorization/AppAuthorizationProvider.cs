using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using AbpNet8;

namespace Admin.Authorization
{
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //admin
            var admin = context.CreatePermission(AppPermissions.Admin, L("Admin"));

            #region HeThong

            var hethong = admin.CreateChildPermission(AppPermissions.Admin_HeThong, L("HeThong"));

            var nguoidung = hethong.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung, L("QuanLyNguoiDung"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_Them, L("Them"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_Sua, L("Sua"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_Xoa, L("Xoa"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_KhoaMoKhoa, L("KhoaMoKhoa"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_DoiMatKhau, L("DoiMatKhau"));

            var qlcctc = hethong.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyCoCauToChuc, L("QuanLyCoCauToChuc"));
            qlcctc.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyCoCauToChuc_ThemToChuc, L("ThemToChuc"));
            qlcctc.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyCoCauToChuc_SuaToChuc, L("SuaToChuc"));
            qlcctc.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyCoCauToChuc_XoaToChuc, L("XoaToChuc"));
            qlcctc.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyCoCauToChuc_ThemNguoiDungToChuc, L("ThemNguoiDungToChuc"));
            qlcctc.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyCoCauToChuc_XoaNguoiDungToChuc, L("XoaNguoiDungToChuc"));

            var vaitro = hethong.CreateChildPermission(AppPermissions.Admin_HeThong_VaiTro, L("VaiTro"));
            vaitro.CreateChildPermission(AppPermissions.Admin_HeThong_VaiTro_Them, L("Them"));
            vaitro.CreateChildPermission(AppPermissions.Admin_HeThong_VaiTro_Sua, L("Sua"));
            vaitro.CreateChildPermission(AppPermissions.Admin_HeThong_VaiTro_Xoa, L("Xoa"));



            #endregion

            #region DanhMuc

            var danhmuc = admin.CreateChildPermission(AppPermissions.Admin_DanhMuc, L("DanhMuc"));

            var danhnghiepruiro = danhmuc.CreateChildPermission(AppPermissions.Admin_DanhMuc_DonViHanhChinh, L("QuanLyRuiRo"));
            danhnghiepruiro.CreateChildPermission(AppPermissions.Admin_DanhMuc_DonViHanhChinh_Them, L("Them"));
            danhnghiepruiro.CreateChildPermission(AppPermissions.Admin_DanhMuc_DonViHanhChinh_Sua, L("Sua"));
            danhnghiepruiro.CreateChildPermission(AppPermissions.Admin_DanhMuc_DonViHanhChinh_Xoa, L("Xoa"));

            #endregion
        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpNet8Consts.LocalizationSourceName);
        }
    }
}
