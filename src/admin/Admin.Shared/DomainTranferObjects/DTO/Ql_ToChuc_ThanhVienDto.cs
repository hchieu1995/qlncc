using Abp.Domain.Entities.Auditing;

namespace Admin.DomainTranferObjects.DTO
{
    public class Ql_ToChuc_ThanhVienDto : FullAuditedEntity
    {
        public long ToChuc_Id { get; set; }
        public long NguoiDung_ThongTin_Id { get; set; }
        public long UserId { get; set; }
    }
}
