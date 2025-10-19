using AbpNet8.MultiTenancy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpNet8.Web.Areas.App.Models.DonVi.ThongTinDonVi
{
    public class DonViIndexViewModel
    {
        public List<TenantListDto> ListTenants { get; set; }
    }
}
