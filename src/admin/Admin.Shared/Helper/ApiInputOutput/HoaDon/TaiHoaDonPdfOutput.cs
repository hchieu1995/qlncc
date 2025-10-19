using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Helper.ApiInputOutput.HoaDon
{
    public class TaiHoaDonPdfOutput
    {
        public string maketqua { get; set; } = "02";
        public string motaketqua { get; set; } = "Không tìm thấy thông tin hóa đơn";
        public string base64pdf { get; set; }
    }
}
