using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Admin.DomainTranferObjects.DTO
{
    public class Ql_CoCauToChucDto : FullAuditedEntity<long>
    {
        public int? ToChuc_CapDo { get; set; }
        public string ToChuc_Ma { get; set; }
        public string ToChuc_Mst { get; set; }
        public string ToChuc_Ten { get; set; }
        public string ToChuc_TenVietTat { get; set; }
        public long? ToChuc_Cha_Id { get; set; }
        public long? ToChuc_Cha_Id_Temp { get; set; }
        public string SpaceLevel { get; set; }
        public int Level { get; set; }

        public List<Ql_CoCauToChucDto> DSToChucCon { get; set; }
    }

    
}
