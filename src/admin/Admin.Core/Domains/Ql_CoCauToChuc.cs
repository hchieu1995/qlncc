using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Domains
{
    [Table("ql_cocautochuc")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class Ql_CoCauToChuc : FullAuditedEntity<long>
    {
        public int? Tc_CapDo { get; set; }
        public string Tc_Ma { get; set; }
        public string Tc_Mst { get; set; }
        public string Tc_Ten { get; set; }
        public string Tc_TenVietTat { get; set; }
        public long? Tc_Cha_Id { get; set; }
    }
}
