using System.Collections.Generic;
using AbpNet8.Roles.Dto;

namespace AbpNet8.Web.Areas.App.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}