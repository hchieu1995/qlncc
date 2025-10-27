using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Domains
{
    [Table("dm_cauhinh")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class Dm_CauHinh: FullAuditedEntity<long>
    {
        public long? User_Id { get; set; }
        public int? ToChuc_Id { get; set; }
        public string CauHinh_Ma { get; set; }
        public string CauHinh_GiaTri { get; set; }
        public string CauHinh_MoTa { get; set; }
    }
    public class MaCauHinh
    {
        public const string EMAIL = "EMAIL";
        public const string KETNOIEMAIL = "KETNOIEMAIL";
        public const string KETNOIBIENLAI = "KETNOIBIENLAI";
        public const string KETNOISMS = "KETNOISMS";
        public const string KETNOIZALO = "KETNOIZALO";
        public const string NGUOIKYTHONGBAO = "NGUOIKYTHONGBAO";
        public const string SOTHONGBAO = "SOTHONGBAO";
        public const string GUITHONGBAO = "GUITHONGBAO";
        public const string MAUTHONGBAO = "MAUTHONGBAO";
    }
    public class CauHinhGuiThongBao
    {
        public const string SMS = "1";
        public const string ZALO = "2";
        public const string EMAIL = "3";
        public const string TATCA = "4";
    }
}
