using System.Threading.Tasks;
using Abp.Application.Services;
using AbpNet8.Authorization.Accounts.Dto;
using AbpNet8.Authorization.Users.Dto;

namespace AbpNet8.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
        Task<ResetPasswordOutput> ResetPassword(ResetPasswordInput input);
    }
}
