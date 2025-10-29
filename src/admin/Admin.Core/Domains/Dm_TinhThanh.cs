using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Domains
{
    [Table("dm_tinhthanh")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class Dm_TinhThanh : FullAuditedEntity
    {
        public string TinhThanh_Ma { get; set; }
        //public string TinhThanh_MaThue { get; set; }
        //public string TinhThanh_MaBh { get; set; }
        public string TinhThanh_Ten { get; set; }
        public string TinhThanh_TenTat { get; set; }
        public DateTime? TinhThanh_BatDau { get; set; }
        public DateTime? TinhThanh_KetThuc { get; set; }
        public bool? TinhThanh_HieuLuc { get; set; }
    }
}
