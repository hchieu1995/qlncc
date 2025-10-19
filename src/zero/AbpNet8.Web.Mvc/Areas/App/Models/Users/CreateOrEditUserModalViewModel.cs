using Abp.Authorization.Users;
using Abp.AutoMapper;
using AbpNet8.Authorization.Users;
using AbpNet8.Authorization.Users.Dto;
using AbpNet8.Dto;
using AbpNet8.Web.Areas.App.Models.Common;
using System.Linq;

namespace AbpNet8.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserForEditOutput))]
    public class CreateOrEditUserModalViewModel : GetUserForEditOutput, IOrganizationUnitsEditViewModel
    {
        public bool CanChangeUserName => User.UserName != AbpUserBase.AdminUserName;

        public int AssignedRoleCount
        {
            get { return Roles.Count(r => r.IsAssigned); }
        }

        public bool IsEditMode => User.Id.HasValue;

        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }
    }
}