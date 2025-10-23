using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Authorization
{
    public static class AppPermissions
    {
        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents = "Pages.DemoUiComponents";

        public const string Admin = "Admin";

        #region DanhMuc

        public const string Admin_DanhMuc = "Admin.DanhMuc";

        public const string Admin_DanhMuc_Khac = "Admin.DanhMuc.Khac";

        public const string Admin_DanhMuc_QuanLyRuiRo = "Admin.DanhMuc.QuanLyRuiRo";
        public const string Admin_DanhMuc_QuanLyRuiRo_CreateNew = "Admin.DanhMuc.QuanLyRuiRo.CreateNew";
        public const string Admin_DanhMuc_QuanLyRuiRo_Update = "Admin.DanhMuc.QuanLyRuiRo.Update";
        public const string Admin_DanhMuc_QuanLyRuiRo_Delete = "Admin.DanhMuc.QuanLyRuiRo.Delete";

        #endregion

        #region HeThong

        public const string Admin_HeThong = "Admin.HeThong";

        public const string Admin_HeThong_NguoiDung = "Admin.HeThong.NguoiDung";
        public const string Admin_HeThong_NguoiDung_CreateNew = "Admin.HeThong.NguoiDung.CreateNew";
        public const string Admin_HeThong_NguoiDung_Update = "Admin.HeThong.NguoiDung.Update";
        public const string Admin_HeThong_NguoiDung_Khoa = "Admin.HeThong.NguoiDung.Khoa";
        public const string Admin_HeThong_NguoiDung_MoKhoa = "Admin.HeThong.NguoiDung.MoKhoa";
        public const string Admin_HeThong_NguoiDung_Delete = "Admin.HeThong.NguoiDung.Delete";
        public const string Admin_HeThong_NguoiDung_DoiMatKhau = "Admin.HeThong.NguoiDung.DoiMatKhau";

        public const string Admin_HeThong_VaiTro = "Admin.HeThong.VaiTro";
        public const string Admin_HeThong_VaiTro_CreateNew = "Admin.HeThong.VaiTro.CreateNew";
        public const string Admin_HeThong_VaiTro_Update = "Admin.HeThong.VaiTro.Update";
        public const string Admin_HeThong_VaiTro_Delete = "Admin.HeThong.VaiTro.Delete";

        public const string Admin_HeThong_QuanLyDoanhNghiep = "Admin.HeThong.QuanLyDoanhNghiep";
        public const string Admin_HeThong_QuanLyDoanhNghiep_DanhSachDoanhNghiep = "Admin.HeThong.QuanLyDoanhNghiep.DanhSachDoanhNghiep";
        public const string Admin_HeThong_QuanLyDoanhNghiep_ThemDoanhNghiep = "Admin.HeThong.QuanLyDoanhNghiep.ThemDoanhNghiep";
        public const string Admin_HeThong_QuanLyDoanhNghiep_XoaDoanhNghiep = "Admin.HeThong.QuanLyDoanhNghiep.XoaDoanhNghiep";
        public const string Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep = "Admin.HeThong.QuanLyDoanhNghiep.SuaDoanhNghiep";

        #endregion


    }
}
