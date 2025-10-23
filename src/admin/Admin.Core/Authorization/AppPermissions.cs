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

        #region HeThong

        public const string Admin_HeThong = "Admin.HeThong";

        public const string Admin_HeThong_NguoiDung = "Admin.HeThong.NguoiDung";
        public const string Admin_HeThong_NguoiDung_Them = "Admin.HeThong.NguoiDung.Them";
        public const string Admin_HeThong_NguoiDung_Sua = "Admin.HeThong.NguoiDung.Sua";
        public const string Admin_HeThong_NguoiDung_Xoa = "Admin.HeThong.NguoiDung.Xoa";
        public const string Admin_HeThong_NguoiDung_KhoaMoKhoa = "Admin.HeThong.NguoiDung.KhoaMoKhoa";
        public const string Admin_HeThong_NguoiDung_DoiMatKhau = "Admin.HeThong.NguoiDung.DoiMatKhau";

        public const string Admin_HeThong_QuanLyCoCauToChuc = "Admin.HeThong.QuanLyCoCauToChuc";
        public const string Admin_HeThong_QuanLyCoCauToChuc_ThemToChuc = "Admin.HeThong.QuanLyCoCauToChuc.ThemToChuc";
        public const string Admin_HeThong_QuanLyCoCauToChuc_SuaToChuc = "Admin.HeThong.QuanLyCoCauToChuc.SuaToChuc";
        public const string Admin_HeThong_QuanLyCoCauToChuc_XoaToChuc = "Admin.HeThong.QuanLyCoCauToChuc.XoaToChuc";
        public const string Admin_HeThong_QuanLyCoCauToChuc_ThemNguoiDungToChuc = "Admin.HeThong.QuanLyCoCauToChuc.ThemNguoiDungToChuc";
        public const string Admin_HeThong_QuanLyCoCauToChuc_XoaNguoiDungToChuc = "Admin.HeThong.QuanLyCoCauToChuc.XoaNguoiDungToChuc";

        public const string Admin_HeThong_VaiTro = "Admin.HeThong.VaiTro";
        public const string Admin_HeThong_VaiTro_Them = "Admin.HeThong.VaiTro.Them";
        public const string Admin_HeThong_VaiTro_Sua = "Admin.HeThong.VaiTro.Sua";
        public const string Admin_HeThong_VaiTro_Xoa = "Admin.HeThong.VaiTro.Xoa";

        

        #endregion

        #region DanhMuc

        public const string Admin_DanhMuc = "Admin.DanhMuc";

        public const string Admin_DanhMuc_Khac = "Admin.DanhMuc.Khac";

        public const string Admin_DanhMuc_QuanLyRuiRo = "Admin.DanhMuc.QuanLyRuiRo";
        public const string Admin_DanhMuc_QuanLyRuiRo_CreateNew = "Admin.DanhMuc.QuanLyRuiRo.CreateNew";
        public const string Admin_DanhMuc_QuanLyRuiRo_Update = "Admin.DanhMuc.QuanLyRuiRo.Update";
        public const string Admin_DanhMuc_QuanLyRuiRo_Delete = "Admin.DanhMuc.QuanLyRuiRo.Delete";

        #endregion

        


    }
}
