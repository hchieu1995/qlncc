using Admin.Shared.DomainTranferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Shared.Helper.Api
{
    public class ThongTinTraCuu
    {

        public int TrangThai { get; set; }
        public string ThongBao { get; set; } = "Không tìm thấy thông tin";
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string QuanHuyen_Ten { get; set; }
        public string TinhThanh_Ten { get; set; }
        public string CoQuanThue_Ten { get; set; }
        public string TinhThanh_Ma { get; set; }
        public string NguoiDaiDien { get; set; }
        public string DienThoai { get; set; }

        public int? tinhthanhid { get; set; }
        public int? quanhuyenid { get; set; }
        public int? coquanthueid { get; set; }

        public List<SelectListItem> DSQuanHuyen { get; set; }
        public List<SelectListItem> DSCoQuanThue { get; set; }

        public string MST { get; set; }
    }
    public class TraCuuOutput
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<ThongTinTraCuuFix> ThongTinTraCuus { get; set; }
    }
    public class ThongTinTraCuuFix
    {
        public string DoanhNghiep_Mst { get; set; }
        public string DoanhNghiep_Ten { get; set; }
        public string DoanhNghiep_DiaChi { get; set; }
    }
}
