using Abp;
using Abp.Domain.Services;
using System.Threading.Tasks;

namespace AbpNet8.Authorization.Users.Profile
{
    public interface IProfileImageService : IDomainService
    {
        Task<string> GetProfilePictureContentForUser(UserIdentifier userIdentifier);
    }
}