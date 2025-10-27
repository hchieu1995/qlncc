using AbpNet8.Paging;
using System.Collections.Generic;

namespace Admin.Model
{
    public class TaiKhoan
    {
        public long UserId { get; set; }
    }
    public class DanhSachTaiKhoanInput : PagedAndSortedInputDto
    {
        public string Filter { get; set; }
        public long ToChucId { get; set; }
    }
    public class ThemMoiTaiKhoanInput
    {
        public long ToChucId { get; set; }
        public List<TaiKhoan> DsTaiKhoan { get; set; }
    }
    
}
