using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DomainTranferObjects.TichHop
{
    public class TaiKhoanCQTInput
    {
        public string doanhnghiep_mst { get; set; }
        public string matkhau { get; set; }
    }
    public class ThongTinDoanhNghiepInput
    {
        public string doanhnghiep_mst { get; set; }
        public string doanhnghiep_ten { get; set; }
        public string doanhnghiep_diachi { get; set; }
        public string doanhnghiep_tinhthanh { get; set; }
        public string doanhnghiep_tinhthanh_ma { get; set; }
        public string doanhnghiep_quanhuyen { get; set; }
        public string doanhnghiep_quanhuyen_ma { get; set; }
        public string doanhnghiep_sdt { get; set; }
        public string doanhnghiep_hotline { get; set; }
        public string doanhnghiep_email { get; set; }
        public string doanhnghiep_fax { get; set; }
        public string doanhnghiep_website { get; set; }
        public string doanhnghiep_nguoidaidien { get; set; }
        public string doanhnghiep_nganhang { get; set; }
        public string doanhnghiep_nguoidaidien_cv { get; set; }
        public string doanhnghiep_nguoidaidien_cccd { get; set; }
        public string doanhnghiep_nguoidaidien_ngaycap { get; set; }
        public string doanhnghiep_nguoidaidien_noicap { get; set; }
        public string doanhnghiep_nganhang_taikhoan { get; set; }
        public List<string> dichvu_ma { get; set; }
        public string doanhnghiep_chicucthue { get; set; }
        public string doanhnghiep_chicucthue_ma { get; set; }
    }
}
