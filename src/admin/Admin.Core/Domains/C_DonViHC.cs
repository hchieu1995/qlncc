using Abp.Domain.Entities;
using Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Domains
{
    [Table("C_DonViHC")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class C_DonViHC : Entity<long>
    {
        public long? IdHC { get; set; }
        public int MaHC { get; set; }
        public string Ten { get; set; }
        public string TenTat { get; set; }
        public int? IdCha { get; set; }
        public string Vung { get; set; }
        public string Cap { get; set; }
        public string Status { get; set; }
        public float? HeSoKV { get; set; }
        public float? LePhi { get; set; }
        public string MaV { get; set; }
        public string FolderExp { get; set; }
        public string ApDung { get; set; }
        public long? Version { get; set; }
        public bool? IsUpdate { get; set; }

    }
}
