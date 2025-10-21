using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace Admin.DomainTranferObjects.DTO
{
    public class Ql_CoCauToChucDto : FullAuditedEntity
    {
        public int? Tc_CapDo { get; set; }
        public string Tc_Ma { get; set; }
        public string Tc_Ten { get; set; }
        public string Tc_TenVietTat { get; set; }
        public long? Tc_Cha_Id { get; set; }
        public string SpaceLevel { get; set; }
        public int Level { get; set; }

        public List<Ql_CoCauToChucDto> DSToChucCon { get; set; }
    }

    
}
