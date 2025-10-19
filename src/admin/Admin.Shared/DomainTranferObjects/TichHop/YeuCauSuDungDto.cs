using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DomainTranferObjects.TichHop
{
    public class YeuCauSuDungDto
    {
        public long? YeuCau_TongTien { get; set; }
        public long? YeuCau_PhiKhac { get; set; }
        public int YeuCau_Loai { get; set; }
        public int YeuCau_TrangThai { get; set; }
        public string YeuCau_GhiChu { get; set; }
        public string YeuCau_LyDoTuChoi { get; set; }
        public string NguoiLienHe_HoTen { get; set; }
        public string DaiLy_Ma { get; set; }
        public string DonVi_MST { get; set; }
        public string DonVi_Ten { get; set; }
        public string CreatorUserName { get; set; }
        public int UpdateDonVi { get; set; }
        public string ApDung_HDDT { get; set; }
        public string Csdl_Ma { get; set; }
        public List<YeuCau_GoiDichVuDto> DSGoiDichVu { get; set; }
        public List<YeuCau_GoiDichVuDto> DSGoiDichVuCu { get; set; }
        public List<NguoiLienHeDto> DSNguoiLienHe { get; set; }
        public DonVi_ThongTinDto ThongTinDonViDto { get; set; }
    }
    public class YeuCau_GoiDichVuDto
    {
        public int GoiDichVu_Id { get; set; }
        public string GoiDichVu_Ma { get; set; }
        public string GoiDichVu_Ten { get; set; }
        public int GoiDichVu_SoLuong { get; set; }
        public long? GoiDichVu_DonGia { get; set; }
        public long? GoiDichVu_ThanhTien { get; set; }
        public int? GoiDichVu_ThoiGianSuDung { get; set; }
        public int? GoiDichVu_ThoiGianLuuTru { get; set; }
        public string DaiLy_Ma { get; set; }
        public string DonVi_MST { get; set; }
        public int YeuCau_Id { get; set; }
    }
    public class DonVi_ThongTinDto
    {
        public string DaiLy_Ma { get; set; }
        public string DonVi_MST { get; set; }
        public string DonVi_MaBHXH { get; set; }
        public string DonVi_Ten { get; set; }
        public string DonViCha_MST { get; set; }
        public string DonVi_DiaChi { get; set; }
        public string DonVi_GiamDoc { get; set; }
        public string ChiCucThue_Ma { get; set; }
        public string ChiCucThue_Ten { get; set; }
        public string DonVi_TinhThanh { get; set; }
        public string DonVi_TinhThanh_Ma { get; set; }
        public string DonVi_QuanHuyen { get; set; }
        public string DonVi_QuanHuyen_Ma { get; set; }
        public string DonVi_CoQuanBHXH_Ma { get; set; }
        public string DonVi_CoQuanBHXH_Ten { get; set; }
        public string HoaDon_SDT { get; set; }
        public string HoaDon_Email { get; set; }
        public string HoaDon_Fax { get; set; }
        public string HoaDon_Website { get; set; }
        public string HoaDon_TaiKhoan { get; set; }
        public string HoaDon_NganHang { get; set; }
        public string HoaDon_DiaChiThongBao { get; set; }
        public string BHXH_PhuongThucDong_Ma { get; set; }
        public string BHXH_LoaiHinh_Ma { get; set; }
        public string BHXH_SDT { get; set; }
        public string BHXH_Email { get; set; }
    }
    public class NguoiLienHeDto
    {
        public int NguoiLienHe_Loai { get; set; }

        public string NguoiLienHe_HoTen { get; set; }

        public string NguoiLienHe_ChucVu { get; set; }

        public string NguoiLienHe_NgaySinh { get; set; }

        public string NguoiLienHe_Email { get; set; }
        public string NguoiLienHe_SDT { get; set; }

        public int NguoiLienHe_GioiTinh { get; set; }

        public string DonVi_MST { get; set; }

        public int YeuCau_Id { get; set; }

        public string DaiLy_Ma { get; set; }
    }
    public class KhoaMoInput
    {
        public string doanhnghiep_mst { get; set; }
        public int? trangthai { get; set; }
    }
}
