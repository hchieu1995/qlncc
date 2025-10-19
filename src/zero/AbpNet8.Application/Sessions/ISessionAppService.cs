using System.Threading.Tasks;
using Abp.Application.Services;
using AbpNet8.Sessions.Dto;

namespace AbpNet8.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
