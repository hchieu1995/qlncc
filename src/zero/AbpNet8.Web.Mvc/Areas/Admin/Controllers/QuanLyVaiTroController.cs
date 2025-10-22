using AbpNet8.Authorization.Permissions;
using AbpNet8.Controllers;
using AbpNet8.Roles.Dto;
using AbpNet8.Web.Areas.App.Models.Roles;
using Admin.AppServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpNet8.Web.Areas.Admin.Controllers
{
    //[AbpMvcAuthorize]
    [Area("Admin")]
    public class QuanLyVaiTroController : AbpNet8ControllerBase
    {
        private readonly QuanLyVaiTroAppService _roleAppService;
        private readonly IPermissionAppService _permissionAppService;

        public QuanLyVaiTroController(
            QuanLyVaiTroAppService roleAppService,
            IPermissionAppService permissionAppService
            )
        {
            _roleAppService = roleAppService;
            _permissionAppService = permissionAppService;
        }
        public IActionResult Index()
        {
            var permissions = _permissionAppService.GetAllPermissions().Items.ToList();
            var model = new RoleListViewModel
            {
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = new List<string>()
            };
            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            var output = await _roleAppService.GetRoleForEdit(id);
            var viewModel = ObjectMapper.Map<CreateOrEditRoleModalViewModel>(output);
            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}
