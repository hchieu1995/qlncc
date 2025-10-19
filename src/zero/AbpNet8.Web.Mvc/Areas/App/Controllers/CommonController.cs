//using Abp.AspNetCore.Mvc.Authorization;
//using AbpNet8.Controllers;
//using AbpNet8.Roles.Dto;
//using AbpNet8.Web.Areas.App.Models.Common.Modals;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    [AbpMvcAuthorize]
//    public class CommonController : AbpNet8ControllerBase
//    {
//        private readonly IPermissionAppService _permissionAppService;

//        public CommonController(IPermissionAppService permissionAppService)
//        {
//            _permissionAppService = permissionAppService;
//        }

//        public PartialViewResult LookupModal(LookupModalViewModel model)
//        {
//            return PartialView("Modals/_LookupModal", model);
//        }

//        public PartialViewResult EntityTypeHistoryModal(EntityHistoryModalViewModel input)
//        {
//            return PartialView("Modals/_EntityTypeHistoryModal", ObjectMapper.Map<EntityHistoryModalViewModel>(input));
//        }

//        public PartialViewResult PermissionTreeModal(List<string> grantedPermissionNames = null)
//        {
//            var permissions = _permissionAppService.GetAllPermissions().Items.ToList();

//            var model = new PermissionTreeModalViewModel
//            {
//                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
//                GrantedPermissionNames = grantedPermissionNames
//            };

//            return PartialView("Modals/_PermissionTreeModal", model);
//        }

//        public PartialViewResult InactivityControllerNotifyModal()
//        {
//            return PartialView("Modals/_InactivityControllerNotifyModal");
//        }
//    }
//}