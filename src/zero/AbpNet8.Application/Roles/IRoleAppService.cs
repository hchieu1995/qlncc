using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AbpNet8.Roles.Dto;

namespace AbpNet8.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task<PagedResultDto<RoleListDto>> GetRoles(GetRolesInput input);

        Task<GetRoleForEditOutput> GetRoleForEdit(int? id);

        Task<Result> CreateOrUpdateRole(CreateOrUpdateRoleInput input);

        Task DeleteRole(int id);
    }
}
