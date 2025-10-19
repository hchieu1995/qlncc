using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AbpNet8.Authorization.Permissions.Dto;

namespace AbpNet8.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
