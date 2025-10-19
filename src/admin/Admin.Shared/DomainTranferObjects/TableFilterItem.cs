using AbpNet8.MultiTenancy.Dto;
using Admin.DomainTranferObjects.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.DomainTranferObjects
{
    public class TableFilterItem
    {
        public int? id { get; set; }
        public string filter { get; set; }
        public string mst { get; set; }
        public string filterext { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public string sort { get; set; }
        public string tinhthanh { get; set; }
        public string quanhuyen { get; set; }
        public int? tenantId { get; set; }
        public int? trangthai { get; set; }
        public string nguoigui { get; set; }
      
        public string NgayNhan_Tu { get; set; }
        public string NgayNhan_Den { get; set; }
    }
    public class TableSorterItem
    {
        public string selector { get; set; }
        public bool desc { get; set; }
    }
    public class TableSearchItem
    {
        public string selector { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }
    public class TableFilterList
    {
        public List<TenantListDto> tenants { get; set; }

    }
    public class EditQuanLyDonViView
    {
        public List<TenantListDto> TenantLists { get; set; }
    }
}
