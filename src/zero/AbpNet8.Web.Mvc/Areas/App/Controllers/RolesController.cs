//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Abp.Application.Services.Dto;
//using Abp.AspNetCore.Mvc.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.Authorization.Permissions;
//using AbpNet8.Authorization.Permissions.Dto;
//using AbpNet8.Authorization.Roles;
//using AbpNet8.Web.Areas.App.Models.Roles;
//using AbpNet8.Web.Controllers;
//using HopDong.Core.Authorization;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    [AbpMvcAuthorize(AppPermissions.Pages)]
//    public class RolesController : AbpNet8ControllerBase
//    {
//        private readonly IRoleAppService _roleAppService;
//        private readonly IPermissionAppService _permissionAppService;

//        public RolesController(
//            IRoleAppService roleAppService,
//            IPermissionAppService permissionAppService)
//        {
//            _roleAppService = roleAppService;
//            _permissionAppService = permissionAppService;
//        }

//        public ActionResult Index()
//        {
//            var permissions = _permissionAppService.GetAllPermissions().Items.ToList();

//            var model = new RoleListViewModel
//            {
//                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
//                GrantedPermissionNames = new List<string>()
//            };

//            return View(model);
//        }

//        public async Task<PartialViewResult> CreateOrEditModal(int? id)
//        {
//            var output = await _roleAppService.GetRoleForEdit(id);
//            var viewModel = ObjectMapper.Map<CreateOrEditRoleModalViewModel>(output);

//            return PartialView("_CreateOrEditModal", viewModel);
//        }
//    }
//}