using System.Collections.Generic;

namespace Admin.Shared.Helper.Api
{
    public class ThongTinMST
    {
        public ThongTinChungMST thongtinchung { get; set; }
        public ThongTinDiaChi diachi { get; set; }
    }

    public class ThongTinChungMST
    {
        public ThongTin row_NNT { get; set; }
    }

    public class ThongTinDiaChi
    {
        public DiaChiChiTiet row_DIACHI { get; set; }
    }

    public class ThongTinTraCuus
    {
        public string doanhNghiep_Mst { get; set; }
        public string doanhNghiep_Ten { get; set; }
        public string doanhNghiep_DiaChi { get; set; }
    }

    public class ThongTinTC
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<ThongTinTraCuus> thongTinTraCuus { get; set; }
    }


    public class ThongTin
    {
        public string maCqThue { get; set; }
        public string tenCqThue { get; set; }
        public string trang_THAI { get; set; }
        public string ngaycap_MST { get; set; }
        public string so { get; set; }
        public string cqt_QL { get; set; }
        public string cap_CHUONG { get; set; }
        public string chuong { get; set; }
        public string chan_DATE { get; set; }
        public string ten_NNT { get; set; }
        public string loai_NNT { get; set; }
        public string mst { get; set; }
    }

    public class DiaChiChiTiet
    {
        public string ten_XA { get; set; }
        public string mota_DIACHI { get; set; }
        public string ma_TINH { get; set; }
        public string ma_HUYEN { get; set; }
        public string ma_XA { get; set; }
        public string ten_TINH { get; set; }
        public string ten_HUYEN { get; set; }
    }
}
