//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Abp;
//using Abp.AspNetCore.Mvc.Authorization;
//using Abp.Auditing;
//using Abp.Domain.Uow;
//using Abp.Extensions;
//using Abp.Runtime.Session;
//using IdentityServer4.Extensions;
//using Microsoft.AspNetCore.Mvc;
//using WebApp.Authorization.Users;
//using WebApp.Authorization.Users.Profile;
//using AbpNet8.Authorization.Users.Profile.Dto;
//using AbpNet8.Authorization.Users.Profile;
//using AbpNet8.Storage;
//using AbpNet8.Net.MimeTypes;
//using AbpNet8.Controllers;

//namespace AbpNet8.Web.Controllers
//{
//    [AbpMvcAuthorize]
//    [DisableAuditing]
//    public class ProfileController : ProfileControllerBase
//    {
//        private readonly IProfileAppService _profileAppService;

//        public ProfileController(
//            ITempFileCacheManager tempFileCacheManager,
//            IProfileAppService profileAppService) : base(tempFileCacheManager, profileAppService)
//        {
//            _profileAppService = profileAppService;
//        }

//        public async Task<FileResult> GetProfilePicture()
//        {
//            var output = await _profileAppService.GetProfilePicture();

//            if (output.ProfilePicture.IsNullOrEmpty())
//            {
//                return GetDefaultProfilePictureInternal();
//            }

//            return File(Convert.FromBase64String(output.ProfilePicture), MimeTypeNames.ImageJpeg);
//        }

//        //public virtual async Task<FileResult> GetFriendProfilePicture(long userId, int? tenantId)
//        //{
//        //    var output = await _profileAppService.GetFriendProfilePicture(new GetFriendProfilePictureInput()
//        //    {
//        //        TenantId = tenantId,
//        //        UserId = userId
//        //    });

//        //    if (output.ProfilePicture.IsNullOrEmpty())
//        //    {
//        //        return GetDefaultProfilePictureInternal();
//        //    }

//        //    return File(Convert.FromBase64String(output.ProfilePicture), MimeTypeNames.ImageJpeg);
//        //}
//    }
//}
