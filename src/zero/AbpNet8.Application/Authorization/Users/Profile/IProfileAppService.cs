using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using AbpNet8.Authorization.Users.Dto;
using AbpNet8.Authorization.Users.Profile.Dto;

namespace AbpNet8.Authorization.Users.Profile
{
    public interface IProfileAppService : IApplicationService
    {
        Task<GetProfilePictureOutput> GetProfilePicture();




    }
}
