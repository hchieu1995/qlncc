using System.Collections.Generic;
using Abp.Application.Services.Dto;
using AbpNet8.MultiTenancy.Dto;

namespace AbpNet8.Web.Areas.App.Models.Settings
{
    public class SettingsViewModel
    {
        public TenantSettingsEditDto Settings { get; set; }
        
        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}