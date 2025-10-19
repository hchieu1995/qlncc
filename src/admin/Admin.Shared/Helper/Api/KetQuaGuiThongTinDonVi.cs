using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Shared.Helper.Api
{
    public class KetQuaGuiThongTinDonVi
    {
        public int maketqua { get; set; }
        public string magiaodich { get; set; }
        public string motaketqua { get; set; }
        public List<TaiKhoanMacDinh> dstaikhoanmacdinh { get; set; }

        public int status { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
        public object data { get; set; }

    }

    public class TaiKhoanMacDinh
    {
        public string taikhoan { get; set; }
        public string matkhau { get; set; }
    }
}
