using Abp.Domain.Entities.Auditing;
using Admin.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.DomainTranferObjects.DTO
{
    public class Dm_TinhThanhDto : FullAuditedEntity
    {
        public string TinhThanh_Ma { get; set; }
        public string TinhThanh_Ten { get; set; }
        public string TinhThanh_TenTat { get; set; }
        public DateTime? TinhThanh_BatDau { get; set; }
        public DateTime? TinhThanh_KetThuc { get; set; }
        public bool? TinhThanh_HieuLuc { get; set; }
        public Dm_TinhThanhDto Dm_TinhThanhDtos { get; set; }
    }
    public class Table_Dm_TinhThanh
    {
        public IQueryable<Dm_TinhThanh> query { get; set; }
        public List<TableSearchItem> search { get; set; }
    }
    public class CreateOrEditTinhThanh
    {
        public Dm_TinhThanhDto Dm_TinhThanhDto { get; set; }
    }
    public class Dm_TinhThanhExcel
    {
        public string Stt { get; set; }
        public string TinhThanh_Ma { get; set; }
        public string TinhThanh_Ten { get; set; }
        public string TinhThanh_TenTat { get; set; }
        public string TinhThanh_BatDau { get; set; }
        public string TinhThanh_KetThuc { get; set; }
        public string TinhThanh_HieuLuc { get; set; }
    }
}
