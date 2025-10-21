using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Domains
{
    [Table("ql_tochuc_thanhvien")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class Ql_ToChuc_ThanhVien : FullAuditedEntity<long>
    {
        public long ToChuc_Id { get; set; }
        public long NguoiDung_ThongTin_Id { get; set; }
        public long UserId { get; set; }
    }
}
