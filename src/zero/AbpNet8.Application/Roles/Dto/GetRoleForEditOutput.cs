using System.Collections.Generic;

namespace AbpNet8.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}