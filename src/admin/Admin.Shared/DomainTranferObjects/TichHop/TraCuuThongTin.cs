using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DomainTranferObjects.TichHop
{
    public class TraThongTinDsHoaDon_RequestDTO
    {
        public string Doanhnghiep_mst { get; set; }
        public int? Db_loai { get; set; }
        public string Nlap { get; set; }
    }

    public class TraThongTinDsHoaDon_HoaDon_ResponseDTO
    {
        public string Mahoadon { get; set; }
        public string Khmshdon { get; set; }
        public string Khhdon { get; set; }
        public string Shdon { get; set; }
        public string Nlap { get; set; }
        public string Nban_NgayKy { get; set; }
        public string Ky_KeKhai { get; set; }
        public string Db_Ngay { get; set; }
        public int? Db_KiemTra_Kq { get; set; }
        public string CanhBao_Ma { get; set; }
        public int? Trangthaicqt { get; set; }
        public string Mccqt { get; set; }
        public int? HoaDon_Loai { get; set; }
        public string Nmua_Ten { get; set; }
        public string Nmua_Mst { get; set; }
        public string Nmua_Dchi { get; set; }
        public string Nban_Ten { get; set; }
        public string Nban_Mst { get; set; }
        public string Nban_Dchi { get; set; }
        public decimal? Tgtkcthue { get; set; }
        public decimal? Tgtthue { get; set; }
        public decimal? Tgtttbso { get; set; }
    }

    public class TraThongTinDsHoaDon_ResponseDTO : TichHop_ResponseDTO
    {
        public List<TraThongTinDsHoaDon_HoaDon_ResponseDTO> Dshoadon { get; set; }
    }


    public class TraThongTinChiTietHD_RequestDTO
    {
        public string Doanhnghiep_mst { get; set; }
        public string Mahoadon { get; set; }
    }

    public class TraThongTinChiTietHD_ChiTiet_ResponseDTO
    {
        public int? Tchat { get; set; }
        public int? Stt { get; set; }
        public string Mhhdvu { get; set; }
        public string Thhdvu { get; set; }
        public string Dvtinh { get; set; }
        public decimal? Sluong { get; set; }
        public decimal? Dgia { get; set; }
        public decimal? Tlckhau { get; set; }
        public decimal? Stckhau { get; set; }
        public decimal? Thtien { get; set; }
        public string Tsuat { get; set; }
    }

    public class TraThongTinChiTietHD_ThueSuat_ResponseDTO
    {
        public string Tsuat { get; set; }
        public decimal? Thtien { get; set; }
        public decimal? Tthue { get; set; }
    }

    public class TraThongTinChiTietHD_LePhi_ResponseDTO
    {
        public string Tlphi { get; set; }
        public decimal? Tphi { get; set; }
    }

    public class TraThongTinChiTietHD_TTKhac_ResponseDTO
    {
        public int? ChiTiet_Id { get; set; }
        public string Ttruong { get; set; }
        public string Kdlieu { get; set; }
        public string Dlieu { get; set; }
    }

    public class TraThongTinChiTietHD_HDKiemTra_ResponseDTO
    {
        public string Ngaycapma { get; set; }
        public string Cts_kyboi_tct { get; set; }
        public string Cts_serial_tct { get; set; }
        public string Cts_ncc_tct { get; set; }
        public string Cts_hieuluc_tungay_tct { get; set; }
        public string Cts_hieuluc_denngay_tct { get; set; }
        public string Cts_mst_nb { get; set; }
        public string Cts_kyboi_nb { get; set; }
        public string Cts_serial_nb { get; set; }
        public string Cts_ncc_nb { get; set; }
        public string Cts_hieuluc_tungay_nb { get; set; }
        public string Cts_hieuluc_denngay_nb { get; set; }
        public string Tct_mahoadon { get; set; }
        public string Dnban_mst { get; set; }
        public string Dnban_trangthai { get; set; }
        public string Dnban_tendonvi { get; set; }
        public string Dnban_diachi { get; set; }
        public string Dnban_cqt { get; set; }
        public string Dnmua_mst { get; set; }
        public string Dnmua_trangthai { get; set; }
        public string Dnmua_tendonvi { get; set; }
        public string Dnmua_diachi { get; set; }
        public string Dnmua_cqt { get; set; }
        public string Loikhongxacdinh { get; set; }
    }

    public class TraThongTinChiTietHD_HoaDon_ResponseDTO
    {
        public string Mahoadon { get; set; }
        public string Khmshdon { get; set; }
        public string Khhdon { get; set; }
        public string Shdon { get; set; }
        public DateTime? Nlap { get; set; }
        public DateTime? Nban_NgayKy { get; set; }
        public string Dvtte { get; set; }
        public decimal? Tgia { get; set; }
        public string Htttoan { get; set; }
        public int? Tchdon { get; set; }
        public int? Lhdclquan { get; set; }
        public string Khmshdclquan { get; set; }
        public string Khhdclquan { get; set; }
        public string Shdclquan { get; set; }
        public DateTime? Nlhdclquan { get; set; }
        public string Gchu { get; set; }
        public DateTime? Db_Ngay { get; set; }
        public int? Db_KiemTra_Kq { get; set; }
        public string CanhBao_Ma { get; set; }
        public int? Trangthaicqt { get; set; }
        public string Mccqt { get; set; }
        public int? HoaDon_Loai { get; set; }
        public string Nmua_Ten { get; set; }
        public string Nmua_Mst { get; set; }
        public string Nmua_Dchi { get; set; }
        public string Nban_Ten { get; set; }
        public string Nban_Mst { get; set; }
        public string Nban_Dchi { get; set; }
        public decimal? Tgtkcthue { get; set; }
        public decimal? Tgtthue { get; set; }
        public decimal? Ttcktmai { get; set; }
        public decimal? Tgtttbso { get; set; }
        public List<TraThongTinChiTietHD_ChiTiet_ResponseDTO> DsChiTiet { get; set; }
        public List<TraThongTinChiTietHD_ThueSuat_ResponseDTO> DsThueSuat { get; set; }
        public List<TraThongTinChiTietHD_LePhi_ResponseDTO> DsLePhi { get; set; }
        public List<TraThongTinChiTietHD_TTKhac_ResponseDTO> DsTTKhac { get; set; }
        public TraThongTinChiTietHD_HDKiemTra_ResponseDTO HoaDon_KiemTra { get; set; }

    }

    public class TraThongTinChiTietHD_ResponseDTO : TichHop_ResponseDTO
    {
        public TraThongTinChiTietHD_HoaDon_ResponseDTO Tthoadon { get; set; }
    }

    public class TaiXMLHoaDon_RequestDTO
    {
        public string Doanhnghiep_mst { get; set; }
        public string Mahoadon { get; set; }
    }

    public class TaiXMLHoaDon_ResponseDTO : TichHop_ResponseDTO
    {
        public string Mahoadon { get; set; }
        public byte[] Xmldata { get; set; }
    }
}
