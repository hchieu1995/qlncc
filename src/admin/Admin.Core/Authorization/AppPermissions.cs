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

        public const string Admin_DanhMuc_Khac_TinhThanh = "Admin.DanhMuc.Khac.TinhThanh";
        public const string Admin_DanhMuc_Khac_TinhThanh_Them = "Admin.DanhMuc.Khac.TinhThanh.Them";
        public const string Admin_DanhMuc_Khac_TinhThanh_Sua = "Admin.DanhMuc.Khac.TinhThanh.Sua";
        public const string Admin_DanhMuc_Khac_TinhThanh_Xoa = "Admin.DanhMuc.Khac.TinhThanh.Xoa";
        public const string Admin_DanhMuc_Khac_TinhThanh_NhapExcel = "Admin.DanhMuc.Khac.TinhThanh.NhapExcel";
        public const string Admin_DanhMuc_Khac_TinhThanh_XuatExcel = "Admin.DanhMuc.Khac.TinhThanh.XuatExcel";

        public const string Admin_DanhMuc_Khac_QuanHuyen = "Admin.DanhMuc.Khac.QuanHuyen";
        public const string Admin_DanhMuc_Khac_QuanHuyen_Them = "Admin.DanhMuc.Khac.QuanHuyen.Them";
        public const string Admin_DanhMuc_Khac_QuanHuyen_Sua = "Admin.DanhMuc.Khac.QuanHuyen.Sua";
        public const string Admin_DanhMuc_Khac_QuanHuyen_Xoa = "Admin.DanhMuc.Khac.QuanHuyen.Xoa";
        public const string Admin_DanhMuc_Khac_QuanHuyen_NhapExcel = "Admin.DanhMuc.Khac.QuanHuyen.NhapExcel";
        public const string Admin_DanhMuc_Khac_QuanHuyen_XuatExcel = "Admin.DanhMuc.Khac.QuanHuyen.XuatExcel";

        public const string Admin_DanhMuc_DanhMucCQT = "Admin.DanhMuc.DanhMucCQT";
        public const string Admin_DanhMuc_DanhMucCQT_ChiCucThue = "Admin.DanhMuc.DanhMucCQT.ChiCucThue";
        public const string Admin_DanhMuc_DanhMucCQT_ChiCucThue_Them = "Admin.DanhMuc.DanhMucCQT.ChiCucThue.Them";
        public const string Admin_DanhMuc_DanhMucCQT_ChiCucThue_Sua = "Admin.DanhMuc.DanhMucCQT.ChiCucThue.Sua";
        public const string Admin_DanhMuc_DanhMucCQT_ChiCucThue_Xoa = "Admin.DanhMuc.DanhMucCQT.ChiCucThue.Xoa";
        public const string Admin_DanhMuc_DanhMucCQT_ChiCucThue_NhapExcel = "Admin.DanhMuc.DanhMucCQT.ChiCucThue.NhapExcel";
        public const string Admin_DanhMuc_DanhMucCQT_ChiCucThue_XuatExcel = "Admin.DanhMuc.DanhMucCQT.ChiCucThue.XuatExcel";

        public const string Admin_DanhMuc_TienTe = "Admin.DanhMuc.TienTe";
        public const string Admin_DanhMuc_TienTe_Them = "Admin.DanhMuc.TienTe.Them";
        public const string Admin_DanhMuc_TienTe_Sua = "Admin.DanhMuc.TienTe.Sua";
        public const string Admin_DanhMuc_TienTe_Xoa = "Admin.DanhMuc.TienTe.Xoa";
        public const string Admin_DanhMuc_TienTe_NhapExcel = "Admin.DanhMuc.TienTe.NhapExcel";
        public const string Admin_DanhMuc_TienTe_XuatExcel = "Admin.DanhMuc.TienTe.XuatExcel";

        public const string Admin_DanhMuc_LoaiFile = "Admin.DanhMuc.LoaiFile";
        public const string Admin_DanhMuc_LoaiFile_Them = "Admin.DanhMuc.LoaiFile.Them";
        public const string Admin_DanhMuc_LoaiFile_Sua = "Admin.DanhMuc.LoaiFile.Sua";
        public const string Admin_DanhMuc_LoaiFile_Xoa = "Admin.DanhMuc.LoaiFile.Xoa";
        public const string Admin_DanhMuc_LoaiFile_Xem = "Admin.DanhMuc.LoaiFile.Xem";

        public const string Admin_DanhMuc_CanhBao = "Admin.DanhMuc.CanhBao";
        public const string Admin_DanhMuc_CanhBao_Them = "Admin.DanhMuc.CanhBao.Them";
        public const string Admin_DanhMuc_CanhBao_Sua = "Admin.DanhMuc.CanhBao.Sua";
        public const string Admin_DanhMuc_CanhBao_Xoa = "Admin.DanhMuc.CanhBao.Xoa";

        public const string Admin_DanhMuc_GoiDichVu = "Admin.DanhMuc.GoiDichVu";
        public const string Admin_DanhMuc_GoiDichVu_Them = "Admin.DanhMuc.GoiDichVu.Them";
        public const string Admin_DanhMuc_GoiDichVu_Sua = "Admin.DanhMuc.GoiDichVu.Sua";
        public const string Admin_DanhMuc_GoiDichVu_Xoa = "Admin.DanhMuc.GoiDichVu.Xoa";
        public const string Admin_DanhMuc_GoiDichVu_NhapExcel = "Admin.DanhMuc.GoiDichVu.NhapExcel";
        public const string Admin_DanhMuc_GoiDichVu_XuatExcel = "Admin.DanhMuc.GoiDichVu.XuatExcel";

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

        public const string Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_GoiDichVu = "Admin_HeThong.QuanLyDoanhNghiep.SuaDoanhNghiep.GoiDichVu";
        public const string Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_GoiDichVu_Them = "Admin.HeThong.QuanLyDoanhNghiep.SuaDoanhNghiep.GoiDichVu.Them";
        public const string Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_GoiDichVu_Xoa = "Admin.HeThong.QuanLyDoanhNghiep.SuaDoanhNghiep.GoiDichVu.Xoa";

        public const string Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_TaiKhoanKetNoi = "Admin.HeThong.QuanLyDoanhNghiep.SuaDoanhNghiep.TaiKhoanKetNoi";
        public const string Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_TaiKhoanKetNoi_Them = "Admin.HeThong.QuanLyDoanhNghiep.SuaDoanhNghiep.TaiKhoanKetNoi.Them";
        public const string Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_TaiKhoanKetNoi_Sua = "Admin.HeThong.QuanLyDoanhNghiep.SuaDoanhNghiep.TaiKhoanKetNoi.Sua";
        public const string Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_TaiKhoanKetNoi_DangNhapDangXuat = "Admin.HeThong.QuanLyDoanhNghiep.SuaDoanhNghiep.TaiKhoanKetNoi.DangNhapDangXuat";

        public const string Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_CauHinh = "Admin.HeThong.QuanLyDoanhNghiep.SuaDoanhNghiep.CauHinh";
        public const string Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_CauHinh_Sua = "Admin.HeThong.QuanLyDoanhNghiep.SuaDoanhNghiep.CauHinh.Sua";

        public const string Admin_HeThong_ThongTinDoanhNghiep = "Admin.HeThong.ThongTinDoanhNghiep";

        public const string Admin_HeThong_ThongTinDoanhNghiep_GoiDichVu = "Admin.HeThong.ThongTinDoanhNghiep.GoiDichVu";
        public const string Admin_HeThong_ThongTinDoanhNghiep_GoiDichVu_Them = "Admin.HeThong.ThongTinDoanhNghiep.GoiDichVu.Them";
        public const string Admin_HeThong_ThongTinDoanhNghiep_GoiDichVu_Xoa = "Admin.HeThong.ThongTinDoanhNghiep.GoiDichVu.Xoa";

        public const string Admin_HeThong_ThongTinDoanhNghiep_TaiKhoanKetNoi = "Admin.HeThong.ThongTinDoanhNghiep.TaiKhoanKetNoi";
        public const string Admin_HeThong_ThongTinDoanhNghiep_TaiKhoanKetNoi_Them = "Admin.HeThong.ThongTinDoanhNghiep.TaiKhoanKetNoi.Them";
        public const string Admin_HeThong_ThongTinDoanhNghiep_TaiKhoanKetNoi_Sua = "Admin.HeThong.ThongTinDoanhNghiep.TaiKhoanKetNoi.Sua";
        public const string Admin_HeThong_ThongTinDoanhNghiep_TaiKhoanKetNoi_DangNhapDangXuat = "Admin.HeThong.ThongTinDoanhNghiep.TaiKhoanKetNoi.DangNhapDangXuat";

        public const string Admin_HeThong_ThongTinDoanhNghiep_CauHinh = "Admin.HeThong.ThongTinDoanhNghiep.CauHinh";
        public const string Admin_HeThong_ThongTinDoanhNghiep_CauHinh_Sua = "Admin.HeThong.ThongTinDoanhNghiep.CauHinh.Sua";

        #endregion

        #region HoaDon

        public const string Admin_HoaDon = "Admin.HoaDon";

        public const string Admin_HoaDon_DauVao = "Admin.HoaDon_DauVao";
        public const string Admin_HoaDon_DauVao_CreateNew = "Admin.HoaDon.DauVao.CreateNew";
        public const string Admin_HoaDon_DauVao_DongBoTuCQT = "Admin.HoaDon.DauVao.DongBoTuCQT";
        public const string Admin_HoaDon_DauVao_CauHinhHienThi = "Admin.HoaDon.DauVao.CauHinhHienThi";
        public const string Admin_HoaDon_DauVao_XuatDLVaoPMKT = "Admin.HoaDon.DauVao.XuatDLVaoPMKT";
        public const string Admin_HoaDon_DauVao_XuatExcelNangCao = "Admin.HoaDon.DauVao.XuatExcelNangCao";
        public const string Admin_HoaDon_DauVao_XemChiTiet = "Admin.HoaDon.DauVao.XemChiTiet";
        public const string Admin_HoaDon_DauVao_TaiPdf = "Admin.HoaDon.DauVao.TaiPdf";
        public const string Admin_HoaDon_DauVao_TaiXml = "Admin.HoaDon.DauVao.TaiXml";
        public const string Admin_HoaDon_DauVao_ChonKyKeKhai = "Admin.HoaDon.DauVao.ChonKyKeKhai";
        public const string Admin_HoaDon_DauVao_DanhDauHopLe = "Admin.HoaDon.DauVao.DanhDauHopLe";
        public const string Admin_HoaDon_DauVao_TaiDanhSachPdf = "Admin.HoaDon.DauVao.TaiDanhSachPdf";
        public const string Admin_HoaDon_DauVao_TaiDanhSachXml = "Admin.HoaDon.DauVao.TaiDanhSachXml";
        public const string Admin_HoaDon_DauVao_InDanhSachHoaDon = "Admin.HoaDon.DauVao.InDanhSachHoaDon";

        public const string Admin_HoaDon_DauRa = "Admin.HoaDon_DauRa";
        public const string Admin_HoaDon_DauRa_CreateNew = "Admin.HoaDon.DauRa.CreateNew";
        public const string Admin_HoaDon_DauRa_DongBoTuCQT = "Admin.HoaDon.DauRa.DongBoTuCQT";
        public const string Admin_HoaDon_DauRa_CauHinhHienThi = "Admin.HoaDon.DauRa.CauHinhHienThi";
        public const string Admin_HoaDon_DauRa_XuatDLVaoPMKT = "Admin.HoaDon.DauRa.XuatDLVaoPMKT";
        public const string Admin_HoaDon_DauRa_XuatExcelNangCao = "Admin.HoaDon.DauRa.XuatExcelNangCao";
        public const string Admin_HoaDon_DauRa_XemChiTiet = "Admin.HoaDon.DauRa.XemChiTiet";
        public const string Admin_HoaDon_DauRa_TaiPdf = "Admin.HoaDon.DauRa.TaiPdf";
        public const string Admin_HoaDon_DauRa_TaiXml = "Admin.HoaDon.DauRa.TaiXml";
        public const string Admin_HoaDon_DauRa_ChonKyKeKhai = "Admin.HoaDon.DauRa.ChonKyKeKhai";
        public const string Admin_HoaDon_DauRa_DanhDauHopLe = "Admin.HoaDon.DauRa.DanhDauHopLe";
        public const string Admin_HoaDon_DauRa_TaiDanhSachPdf = "Admin.HoaDon.DauRa.TaiDanhSachPdf";
        public const string Admin_HoaDon_DauRa_TaiDanhSachXml = "Admin.HoaDon.DauRa.TaiDanhSachXml";

        public const string Admin_HoaDon_LichSuDongBo = "Admin.HoaDon.LichSuDongBo";

        public const string Admin_HoaDon_QuanLyTaiLieu = "Admin.HoaDon.QuanLyTaiLieu";
        public const string Admin_HoaDon_QuanLyTaiLieu_CreateNew = "Admin.HoaDon.QuanLyTaiLieu.CreateNew";
        public const string Admin_HoaDon_QuanLyTaiLieu_Update = "Admin.HoaDon.QuanLyTaiLieu.Update";
        public const string Admin_HoaDon_QuanLyTaiLieu_Delete = "Admin.HoaDon.QuanLyTaiLieu.Delete";
        public const string Admin_HoaDon_QuanLyTaiLieu_TaiTaiLieu = "Admin.HoaDon.QuanLyTaiLieu.TaiTaiLieu";

        #endregion

        #region HopThu

        public const string Admin_HopThu = "Admin.HopThu";

        public const string Admin_HopThu_DanhSachMail = "Admin.HopThu.DanhSachMail";
        public const string Admin_HopThu_DanhSachMail_DocMail = "Admin.HopThu.DanhSachMail.DocMail";
        public const string Admin_HopThu_DanhSachMail_Xem = "Admin.HopThu.DanhSachMail.Xem";
        public const string Admin_HopThu_DanhSachMail_Xoa = "Admin.HopThu.DanhSachMail.Xoa";

        public const string Admin_HopThu_LichSuDocMail = "Admin.HopThu.LichSuDocMail";

        #endregion

        #region BaoCao

        public const string Admin_BaoCao = "Admin.BaoCao";

        public const string Admin_BaoCao_TrangThaiHoaDon = "Admin.BaoCao.TrangThaiHoaDon";
        public const string Admin_BaoCao_TrangThaiHoaDon_XuatExcel = "Admin.BaoCao.TrangThaiHoaDon.XuatExcel";

        public const string Admin_BaoCao_ToKhaiThueGTGT = "Admin.BaoCao.ToKhaiThueGTGT";
        public const string Admin_BaoCao_ToKhaiThueGTGT_XuatExcel = "Admin.BaoCao.ToKhaiThueGTGT.XuatExcel";

        public const string Admin_BaoCao_ToKhaiGiamThue = "Admin.BaoCao.ToKhaiGiamThue";
        public const string Admin_BaoCao_ToKhaiGiamThue_XuatExcel = "Admin.BaoCao.ToKhaiGiamThue.XuatExcel";

        public const string Admin_BaoCao_BangKeHoaDon = "Admin.BaoCao.BangKeHoaDon";
        public const string Admin_BaoCao_BangKeHoaDon_XuatExcel = "Admin.BaoCao.BangKeHoaDon.XuatExcel";

        #endregion

    }
}
