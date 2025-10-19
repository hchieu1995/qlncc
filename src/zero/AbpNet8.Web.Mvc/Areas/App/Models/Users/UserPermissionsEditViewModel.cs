using Abp.AutoMapper;
using AbpNet8.Authorization.Users;
using AbpNet8.Authorization.Users.Dto;
using AbpNet8.Web.Areas.App.Models.Common;

namespace AbpNet8.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}