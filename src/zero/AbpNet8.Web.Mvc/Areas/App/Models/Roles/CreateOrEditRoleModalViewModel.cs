using Abp.AutoMapper;
using AbpNet8.Roles.Dto;
using AbpNet8.Web.Areas.App.Models.Common;

namespace AbpNet8.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id > 0;
    }
}