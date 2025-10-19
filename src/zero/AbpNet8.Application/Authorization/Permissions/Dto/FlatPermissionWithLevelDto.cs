using AbpNet8.Roles.Dto;

namespace AbpNet8.Authorization.Permissions.Dto
{
    public class FlatPermissionWithLevelDto: FlatPermissionDto
    {
        public int Level { get; set; }
    }
}
