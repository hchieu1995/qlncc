using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using AbpNet8.Dto;

namespace AbpNet8.Authorization.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
