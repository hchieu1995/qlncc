using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Helper.ApiInputOutput.HoaDon
{
    public class GuiVaKyHoadonGocHSMInput
    {
        public string doanhnghiep_mst { get; set; }
        public string loai_hd_dacthu { get; set; }
        public string loaihoadon_ma { get; set; }
        public string mauso { get; set; }
        public string kyhieu { get; set; }
        public string ma_hoadon { get; set; }
        public string ngaylap { get; set; }
        public string dnmua_mst { get; set; }
        public string dnmua_ten { get; set; }
        public string dnmua_tennguoimua { get; set; }
        public string dnmua_diachi { get; set; }
        public string dnmua_sdt { get; set; }
        public string dnmua_email { get; set; }
        public string dnmua_diachigiaohang { get; set; }
        public int? thanhtoan_phuongthuc { get; set; }
        public string thanhtoan_phuongthuc_ten { get; set; }
        public string thanhtoan_taikhoan { get; set; }
        public string thanhtoan_nganhang { get; set; }
        public string tiente_ma { get; set; }
        public decimal? tygiangoaite { get; set; }
        public string thanhtoan_thoihan { get; set; }
        public string tongtien_chietkhau { get; set; }
        public string ghichu { get; set; }
        public string tongtien_chuavat { get; set; }
        public string tienthue { get; set; }
        public string tongtien_covat { get; set; }
        public string nguoilap { get; set; }
        public string benhvien_loaidichvu { get; set; }
        public string hanghai_tentau { get; set; }
        public string hanghai_dungtichthanhphan { get; set; }
        public string hanghai_loaitau { get; set; }
        public string hanghai_quydoi { get; set; }
        public string chutienganh { get; set; }
        public string hanghai_ngayden { get; set; }
        public string hanghai_ngaydi { get; set; }
        public string dnmua_quocgia { get; set; }
        public string chutiengviet { get; set; }
        public string vanchuyen_so { get; set; }
        public string vanchuyen_ngayxuat { get; set; }
        public string vanchuyen_dieudong { get; set; }
        public string vanchuyen_giaohang { get; set; }
        public string vanchuyen_khoxuat { get; set; }
        public string vanchuyen_khonhap { get; set; }
        public string vanchuyen_phuongthuc { get; set; }
        public string vanchuyen_lydo { get; set; }
        public string vanchuyen_lenh { get; set; }
        public string dnban_tc_ma { get; set; }
        public string dnban_tc_ten { get; set; }
        public string dnban_tc_dienthoai { get; set; }
        public string dnban_tc_fax { get; set; }
        public string dnban_tc_email { get; set; }
        public string dnban_tc_diachi { get; set; }
        public string dnban_tc_taikhoan { get; set; }
        public string dnban_tc_nganhang { get; set; }
        public decimal? vmb_thuhothuekhac { get; set; }
        public decimal? vmb_thuhophikhac { get; set; }
        public decimal? vmb_banvephidv { get; set; }
        public int? vmb_banvemathuedv { get; set; }
        public decimal? vmb_banvetienthuedv { get; set; }
        public string sophieu { get; set; }
        public string sp_sonb { get; set; }
        public string sp_soddh { get; set; }
        public string sp_sonoiboxnk { get; set; }
        public string sp_nguoitao_ngaytao { get; set; }
        public string sp_sohopdong { get; set; }
        public string sp_trinhduocvien { get; set; }
        public string sp_nhanviengiaohang { get; set; }
        public string sp_sokhoan { get; set; }

        public decimal? phikhac_tyle { get; set; }
        public decimal? phikhac_sotien { get; set; }
        public string dulieudacthu01 { get; set; }
        public string dulieudacthu02 { get; set; }
        public string dulieudacthu03 { get; set; }
        public string dulieudacthu04 { get; set; }
        public string dulieudacthu05 { get; set; }
        public string dulieudacthu06 { get; set; }
        public string dulieudacthu07 { get; set; }
        public string dulieudacthu08 { get; set; }
        public string dulieudacthu09 { get; set; }
        public string dulieudacthu10 { get; set; }
        public decimal? tonghop_tienthuegtgtgiam { get; set; }
        public decimal? tonghop_thuedoanhthu { get; set; }
        public List<GuiVaKyHoadonGocHSMInput_Dschitiet> dschitiet { get; set; }
        public List<GuiVaKyHoadonGocHSMInput_Dsthuesuat> dsthuesuat { get; set; }
    }
    public class GuiVaKyHoadonGocHSMInput_Dschitiet
    {
        public int? stt { get; set; }
        public int? hanghoa_loai { get; set; }
        public int? khuyenmai { get; set; }
        public string ma { get; set; }
        public string ten { get; set; }
        public string donvitinh { get; set; }
        public decimal? soluong { get; set; }
        public decimal? dongia { get; set; }
        public decimal? phantram_chietkhau { get; set; }
        public decimal? tongtien_chietkhau { get; set; }
        public decimal? tongtien_chuathue { get; set; }
        public string mathue { get; set; }
        public decimal? tongtien_cothue { get; set; }
        public decimal? mucbhtra { get; set; }
        public decimal? bhtra { get; set; }
        public decimal? benhnhantra { get; set; }
        public string ghichu { get; set; }
        public string thuoc_solo { get; set; }
        public string thuoc_hansudung { get; set; }
        public decimal? thuoc_chietkhau2 { get; set; }
        public int? vanchuyen_loai { get; set; }

        public decimal? phikhac_tyle { get; set; }
        public decimal? phikhac_sotien { get; set; }
        public string dulieudacthu01 { get; set; }
        public string dulieudacthu02 { get; set; }
        public string dulieudacthu03 { get; set; }
        public string dulieudacthu04 { get; set; }
        public string dulieudacthu05 { get; set; }
        public string dulieudacthu06 { get; set; }
        public string dulieudacthu07 { get; set; }
        public string dulieudacthu08 { get; set; }
        public string dulieudacthu09 { get; set; }
        public string dulieudacthu10 { get; set; }
    }
    public class GuiVaKyHoadonGocHSMInput_Dsthuesuat
    {
        public string mathue { get; set; }
        public decimal? tongtien_chiuthue { get; set; }
        public decimal? tongtien_thue { get; set; }
    }
    public class GuiVaKyHoadonGocHSMOutput
    {
        public string maketqua { get; set; }
        public string motaketqua { get; set; }
        public string magiaodich { get; set; }
        public string ma_hoadon { get; set; }
        public string mauso { get; set; }
        public string kyhieu { get; set; }
        public string sohoadon { get; set; }
        public string trangthai { get; set; }
        public string ngayky { get; set; }
        public string loaihoadon { get; set; }
        public string chuyendoi { get; set; }
        public string base64xml { get; set; }
    }
}
