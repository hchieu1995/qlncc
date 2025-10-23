using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Domains
{
    [Table("ql_cocautochuc")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class Ql_CoCauToChuc : FullAuditedEntity<long>
    {
        public int? ToChuc_CapDo { get; set; }
        public string ToChuc_Ma { get; set; }
        public string ToChuc_Mst { get; set; }
        public string ToChuc_Ten { get; set; }
        public string ToChuc_TenVietTat { get; set; }
        public long? ToChuc_Cha_Id { get; set; }
    }
}
