using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Domains
{
    [Table("nguoidung_thongtin")]
    public class NguoiDung_ThongTin : FullAuditedEntity<long>
    {
        public long Id { get; set; }
        public string NguoiDung_ToChuc_Ma { get; set; }
        public long UserId { get; set; }
        public string NguoiDung_TaiKhoan { get; set; }
        public string NguoiDung_MatKhau { get; set; }
        public string NguoiDung_HoTen { get; set; }
        public string NguoiDung_Sdt { get; set; }
        public string NguoiDung_Email { get; set; }
        public bool? NguoiDung_TrangThai { get; set; }

        public string TokenValidityKey { get; set; }
        public string AccessToken { get; set; }
        public DateTime? ExpireTime { get; set; }
    }
}
