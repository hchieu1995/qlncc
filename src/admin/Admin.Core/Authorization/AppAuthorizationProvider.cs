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

            #region DanhMuc

            var danhmuc = admin.CreateChildPermission(AppPermissions.Admin_DanhMuc, L("DanhMuc"));

            var danhnghiepruiro = danhmuc.CreateChildPermission(AppPermissions.Admin_DanhMuc_QuanLyRuiRo, L("QuanLyRuiRo"));
            danhnghiepruiro.CreateChildPermission(AppPermissions.Admin_DanhMuc_QuanLyRuiRo_CreateNew, L("CreateNew"));
            danhnghiepruiro.CreateChildPermission(AppPermissions.Admin_DanhMuc_QuanLyRuiRo_Update, L("Update"));
            danhnghiepruiro.CreateChildPermission(AppPermissions.Admin_DanhMuc_QuanLyRuiRo_Delete, L("Delete"));

            #endregion

            #region HeThong

            var hethong = admin.CreateChildPermission(AppPermissions.Admin_HeThong, L("HeThong"));

            var nguoidung = hethong.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung, L("QuanLyNguoiDung"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_CreateNew, L("CreateNew"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_Update, L("Update"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_Khoa, L("Khoa"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_MoKhoa, L("MoKhoa"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_Delete, L("Delete"));
            nguoidung.CreateChildPermission(AppPermissions.Admin_HeThong_NguoiDung_DoiMatKhau, L("DoiMatKhau"));

            var vaitro = hethong.CreateChildPermission(AppPermissions.Admin_HeThong_VaiTro, L("VaiTro"));
            vaitro.CreateChildPermission(AppPermissions.Admin_HeThong_VaiTro_CreateNew, L("CreateNew"));
            vaitro.CreateChildPermission(AppPermissions.Admin_HeThong_VaiTro_Update, L("Update"));
            vaitro.CreateChildPermission(AppPermissions.Admin_HeThong_VaiTro_Delete, L("Delete"));

            var qldn = hethong.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep, L("QuanLyDoanhNghiep"));
            qldn.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_DanhSachDoanhNghiep, L("DanhSachDoanhNghiep"));
            qldn.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_ThemDoanhNghiep, L("ThemDoanhNghiep"));
            qldn.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_XoaDoanhNghiep, L("XoaDoanhNghiep"));

            #endregion

        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpNet8Consts.LocalizationSourceName);
        }
    }
}
