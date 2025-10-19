using System.Collections.Generic;
using Abp.Application.Services.Dto;
using AbpNet8.MultiTenancy.Dto;
using AbpNet8.Roles.Dto;
using AbpNet8.Web.Areas.App.Models.Common;

namespace AbpNet8.Web.Areas.App.Models.Users
{
    public class UsersViewModel : IPermissionsEditViewModel
    {
        public string FilterText { get; set; }

        public List<ComboboxItemDto> Roles { get; set; }

        public bool OnlyLockedUsers { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
        public List<TenantListDto> DSTenant { get; set; }
    }
}