using System.Collections.Generic;
using Abp.Application.Services.Dto;
using AbpNet8.Roles.Dto;
using AbpNet8.Web.Areas.App.Models.Common;

namespace AbpNet8.Web.Areas.App.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}