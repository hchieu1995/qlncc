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

            var dmkhac = danhmuc.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac, L("DanhMucKhac"));

            var TinhThanh = dmkhac.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_TinhThanh, L("TinhThanh"));
            TinhThanh.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_TinhThanh_Them, L("Them"));
            TinhThanh.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_TinhThanh_Sua, L("Sua"));
            TinhThanh.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_TinhThanh_Xoa, L("Xoa"));
            TinhThanh.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_TinhThanh_NhapExcel, L("NhapExcel"));
            TinhThanh.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_TinhThanh_XuatExcel, L("XuatExcel"));

            var QuanHuyen = dmkhac.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_QuanHuyen, L("QuanHuyen"));
            QuanHuyen.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_QuanHuyen_Them, L("Them"));
            QuanHuyen.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_QuanHuyen_Sua, L("Sua"));
            QuanHuyen.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_QuanHuyen_Xoa, L("Xoa"));
            QuanHuyen.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_QuanHuyen_NhapExcel, L("NhapExcel"));
            QuanHuyen.CreateChildPermission(AppPermissions.Admin_DanhMuc_Khac_QuanHuyen_XuatExcel, L("XuatExcel"));

            danhmuc.CreateChildPermission(AppPermissions.Admin_DanhMuc_DanhMucCQT, L("DanhMucCQT"));
            var dmcqt = danhmuc.CreateChildPermission(AppPermissions.Admin_DanhMuc_DanhMucCQT_ChiCucThue, L("ChiCucThue"));
            dmcqt.CreateChildPermission(AppPermissions.Admin_DanhMuc_DanhMucCQT_ChiCucThue_Them, L("Them"));
            dmcqt.CreateChildPermission(AppPermissions.Admin_DanhMuc_DanhMucCQT_ChiCucThue_Sua, L("Sua"));
            dmcqt.CreateChildPermission(AppPermissions.Admin_DanhMuc_DanhMucCQT_ChiCucThue_Xoa, L("Xoa"));
            dmcqt.CreateChildPermission(AppPermissions.Admin_DanhMuc_DanhMucCQT_ChiCucThue_NhapExcel, L("NhapExcel"));
            dmcqt.CreateChildPermission(AppPermissions.Admin_DanhMuc_DanhMucCQT_ChiCucThue_XuatExcel, L("XuatExcel"));

            var TienTe = danhmuc.CreateChildPermission(AppPermissions.Admin_DanhMuc_TienTe, L("DanhMucTienTe"));
            TienTe.CreateChildPermission(AppPermissions.Admin_DanhMuc_TienTe_Them, L("Them"));
            TienTe.CreateChildPermission(AppPermissions.Admin_DanhMuc_TienTe_Sua, L("Sua"));
            TienTe.CreateChildPermission(AppPermissions.Admin_DanhMuc_TienTe_Xoa, L("Xoa"));
            TienTe.CreateChildPermission(AppPermissions.Admin_DanhMuc_TienTe_NhapExcel, L("NhapExcel"));
            TienTe.CreateChildPermission(AppPermissions.Admin_DanhMuc_TienTe_XuatExcel, L("XuatExcel"));

            var LoaiFile = danhmuc.CreateChildPermission(AppPermissions.Admin_DanhMuc_LoaiFile, L("DanhMucLoaiFile"));
            LoaiFile.CreateChildPermission(AppPermissions.Admin_DanhMuc_LoaiFile_Them, L("Them"));
            LoaiFile.CreateChildPermission(AppPermissions.Admin_DanhMuc_LoaiFile_Sua, L("Sua"));
            LoaiFile.CreateChildPermission(AppPermissions.Admin_DanhMuc_LoaiFile_Xoa, L("Xoa"));
            LoaiFile.CreateChildPermission(AppPermissions.Admin_DanhMuc_LoaiFile_Xem, L("Xem"));

            var CanhBao = danhmuc.CreateChildPermission(AppPermissions.Admin_DanhMuc_CanhBao, L("DanhMucCanhBao"));
            CanhBao.CreateChildPermission(AppPermissions.Admin_DanhMuc_CanhBao_Them, L("Them"));
            CanhBao.CreateChildPermission(AppPermissions.Admin_DanhMuc_CanhBao_Sua, L("Sua"));
            CanhBao.CreateChildPermission(AppPermissions.Admin_DanhMuc_CanhBao_Xoa, L("Xoa"));

            var dmgdv = danhmuc.CreateChildPermission(AppPermissions.Admin_DanhMuc_GoiDichVu, L("DanhMucGoiDichVu"));
            dmgdv.CreateChildPermission(AppPermissions.Admin_DanhMuc_GoiDichVu_Them, L("Them"));
            dmgdv.CreateChildPermission(AppPermissions.Admin_DanhMuc_GoiDichVu_Sua, L("Sua"));
            dmgdv.CreateChildPermission(AppPermissions.Admin_DanhMuc_GoiDichVu_Xoa, L("Xoa"));
            dmgdv.CreateChildPermission(AppPermissions.Admin_DanhMuc_GoiDichVu_NhapExcel, L("NhapExcel"));
            dmgdv.CreateChildPermission(AppPermissions.Admin_DanhMuc_GoiDichVu_XuatExcel, L("XuatExcel"));

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
            var suadn = qldn.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep, L("SuaDoanhNghiep"));

            var cauhinh = suadn.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_CauHinh, L("CauHinh"));
            cauhinh.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_CauHinh_Sua, L("Sua"));

            var gdv = suadn.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_GoiDichVu, L("GoiDichVu"));
            gdv.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_GoiDichVu_Them, L("Them"));
            gdv.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_GoiDichVu_Xoa, L("Xoa"));

            var taikhoanknn = suadn.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_TaiKhoanKetNoi, L("TaiKhoanKetNoi"));
            taikhoanknn.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_TaiKhoanKetNoi_Them, L("Them"));
            taikhoanknn.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_TaiKhoanKetNoi_Sua, L("Sua"));
            taikhoanknn.CreateChildPermission(AppPermissions.Admin_HeThong_QuanLyDoanhNghiep_SuaDoanhNghiep_TaiKhoanKetNoi_DangNhapDangXuat, L("DangNhapDangXuat"));

            
            var ttdn = hethong.CreateChildPermission(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep, L("ThongTinDoanhNghiep"));

            var cauhinh_ttdn = ttdn.CreateChildPermission(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep_CauHinh, L("CauHinh"));
            cauhinh_ttdn.CreateChildPermission(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep_CauHinh_Sua, L("Sua"));

            var gdv_ttdn = ttdn.CreateChildPermission(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep_GoiDichVu, L("GoiDichVu"));
            gdv_ttdn.CreateChildPermission(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep_GoiDichVu_Them, L("Them"));
            gdv_ttdn.CreateChildPermission(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep_GoiDichVu_Xoa, L("Xoa"));

            var taikhoanknn_ttdn = ttdn.CreateChildPermission(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep_TaiKhoanKetNoi, L("TaiKhoanKetNoi"));
            taikhoanknn_ttdn.CreateChildPermission(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep_TaiKhoanKetNoi_Them, L("Them"));
            taikhoanknn_ttdn.CreateChildPermission(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep_TaiKhoanKetNoi_Sua, L("Sua"));
            taikhoanknn_ttdn.CreateChildPermission(AppPermissions.Admin_HeThong_ThongTinDoanhNghiep_TaiKhoanKetNoi_DangNhapDangXuat, L("DangNhapDangXuat"));

            

            #endregion

            #region HoaDon

            var hoadon = admin.CreateChildPermission(AppPermissions.Admin_HoaDon, L("HoaDon"));

            var hoadondauvao = hoadon.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao, L("HoaDonDauVao"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_CreateNew, L("CreateNew"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_DongBoTuCQT, L("DongBoTuCQT"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_CauHinhHienThi, L("CauHinhHienThi"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_XuatDLVaoPMKT, L("XuatDLVaoPMKT"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_XuatExcelNangCao, L("XuatExcelNangCao"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_XemChiTiet, L("XemChiTiet"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_TaiPdf, L("TaiPdf"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_TaiXml, L("TaiXml"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_ChonKyKeKhai, L("ChonKyKeKhai"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_DanhDauHopLe, L("DanhDauHopLe"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_TaiDanhSachPdf, L("TaiDanhSachPdf"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_TaiDanhSachXml, L("TaiDanhSachXml"));
            hoadondauvao.CreateChildPermission(AppPermissions.Admin_HoaDon_DauVao_InDanhSachHoaDon, L("InDanhSachHoaDon"));

            var hoadondaura = hoadon.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa, L("HoaDonDauRa"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_CreateNew, L("CreateNew"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_DongBoTuCQT, L("DongBoTuCQT"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_CauHinhHienThi, L("CauHinhHienThi"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_XuatDLVaoPMKT, L("XuatDLVaoPMKT"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_XuatExcelNangCao, L("XuatExcelNangCao"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_XemChiTiet, L("XemChiTiet"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_TaiPdf, L("TaiPdf"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_TaiXml, L("TaiXml"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_ChonKyKeKhai, L("ChonKyKeKhai"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_DanhDauHopLe, L("DanhDauHopLe"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_TaiDanhSachPdf, L("TaiDanhSachPdf"));
            hoadondaura.CreateChildPermission(AppPermissions.Admin_HoaDon_DauRa_TaiDanhSachXml, L("TaiDanhSachXml"));

            var lsdb = hoadon.CreateChildPermission(AppPermissions.Admin_HoaDon_LichSuDongBo, L("LichSuDongBo"));

            var qltl = hoadon.CreateChildPermission(AppPermissions.Admin_HoaDon_QuanLyTaiLieu, L("QuanLyTaiLieu"));
            qltl.CreateChildPermission(AppPermissions.Admin_HoaDon_QuanLyTaiLieu_CreateNew, L("CreateNew"));
            qltl.CreateChildPermission(AppPermissions.Admin_HoaDon_QuanLyTaiLieu_Update, L("Update"));
            qltl.CreateChildPermission(AppPermissions.Admin_HoaDon_QuanLyTaiLieu_TaiTaiLieu, L("TaiTaiLieu"));
            qltl.CreateChildPermission(AppPermissions.Admin_HoaDon_QuanLyTaiLieu_Delete, L("Delete"));

            #endregion

            #region HopThu

            var hopthu = admin.CreateChildPermission(AppPermissions.Admin_HopThu, L("HopThu"));

            var dsmail = hopthu.CreateChildPermission(AppPermissions.Admin_HopThu_DanhSachMail, L("DanhSachMail"));
            dsmail.CreateChildPermission(AppPermissions.Admin_HopThu_DanhSachMail_DocMail, L("DocMail"));
            dsmail.CreateChildPermission(AppPermissions.Admin_HopThu_DanhSachMail_Xem, L("Xem"));
            dsmail.CreateChildPermission(AppPermissions.Admin_HopThu_DanhSachMail_Xoa, L("Xoa"));

            var lsdm = hopthu.CreateChildPermission(AppPermissions.Admin_HopThu_LichSuDocMail, L("LichSuDocMail"));

            #endregion

            #region BaoCao

            var baocao = admin.CreateChildPermission(AppPermissions.Admin_BaoCao, L("BaoCao"));

            var trangthaihoadon = baocao.CreateChildPermission(AppPermissions.Admin_BaoCao_TrangThaiHoaDon, L("TrangThaiHoaDon"));
            trangthaihoadon.CreateChildPermission(AppPermissions.Admin_BaoCao_TrangThaiHoaDon_XuatExcel, L("XuatExcel"));

            var tokhaithuegtgt = baocao.CreateChildPermission(AppPermissions.Admin_BaoCao_ToKhaiThueGTGT, L("ToKhaiThueGTGT"));
            tokhaithuegtgt.CreateChildPermission(AppPermissions.Admin_BaoCao_ToKhaiThueGTGT_XuatExcel, L("XuatExcel"));

            var tokhaigiamthue = baocao.CreateChildPermission(AppPermissions.Admin_BaoCao_ToKhaiGiamThue, L("ToKhaiGiamThue"));
            tokhaigiamthue.CreateChildPermission(AppPermissions.Admin_BaoCao_ToKhaiGiamThue_XuatExcel, L("XuatExcel"));

            var bangkehoadon = baocao.CreateChildPermission(AppPermissions.Admin_BaoCao_BangKeHoaDon, L("BangKeHoaDon"));
            bangkehoadon.CreateChildPermission(AppPermissions.Admin_BaoCao_BangKeHoaDon_XuatExcel, L("XuatExcel"));
            #endregion

        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpNet8Consts.LocalizationSourceName);
        }
    }
}
