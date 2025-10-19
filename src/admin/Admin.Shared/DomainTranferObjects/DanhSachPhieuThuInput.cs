using Admin.DomainTranferObjects.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.DomainTranferObjects
{
    public class DanhSachPhieuThuInput
    {
        public string TaiKhoan { get; set; }
        public string Text { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
    }
}
