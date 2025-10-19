using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DomainTranferObjects.TichHop
{
    public class TaiKhoanCQTOutput
    {
        public string maketqua { get; set; }
        public string motaketqua { get; set; }
    }
    public class ThongTinDoanhNghiepOutput
    {
        public string maketqua { get; set; }
        public string magiaodich { get; set; }
        public string motaketqua { get; set; }
        public List<TaiKhoanMacDinh> dstaikhoanmacdinh { get; set; }

    }
    public class TaiKhoanMacDinh
    {
        public string taikhoan { get; set; }
        public string matkhau { get; set; }
    }
    public class ResultDuyet
    {
        public string maketqua { get; set; }
        public string motaketqua { get; set; }
    }
}
