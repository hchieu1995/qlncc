using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Helper.ApiInputOutput.HoaDon
{
    public class GuiXmlDaKyInput
    {
        public string doanhnghiep_mst { get; set; }
        public string magiaodich { get; set; }
        public string ma_hoadon { get; set; }
        public string base64xml { get; set; }
        public string maphiengiaodich { get; set; }
        public int id { get; set; }
    }
    public class GuiXmlDaKyOutput
    {
        public string maketqua { get; set; } = "02";
        public string motaketqua { get; set; } = "Không tìm thấy thông tin hóa đơn";
        public string mahoadon { get; set; }
        public string mauso { get; set; }
        public string kyhieu { get; set; }
        public string sohoadon { get; set; }
        public string ngaylap { get; set; }
        public string magiaodich { get; set; }
    }
}
