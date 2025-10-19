//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Abp.Application.Services.Dto;
//using Abp.AspNetCore.Mvc.Authorization;
//using Microsoft.AspNetCore.Mvc;

//using AbpNet8.Authorization.Permissions;
//using AbpNet8.Authorization.Permissions.Dto;
//using AbpNet8.Authorization.Roles;
//using AbpNet8.Authorization.Roles.Dto;
//using AbpNet8.Authorization.Users;
//using AbpNet8.Security;
//using AbpNet8.Web.Areas.App.Models.Roles;
//using AbpNet8.Web.Areas.App.Models.Users;
//using AbpNet8.Web.Controllers;
//using HopDong.Core.Authorization;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    [AbpMvcAuthorize(AppPermissions.Pages)]
//    public class UsersController : AbpNet8ControllerBase
//    {
//        private readonly IUserAppService _userAppService;
//        private readonly UserManager _userManager;
//        private readonly IUserLoginAppService _userLoginAppService;
//        private readonly IRoleAppService _roleAppService;
//        private readonly IPermissionAppService _permissionAppService;
//        private readonly IPasswordComplexitySettingStore _passwordComplexitySettingStore;

//        public UsersController(
//            IUserAppService userAppService,
//            UserManager userManager,
//            IUserLoginAppService userLoginAppService,
//            IRoleAppService roleAppService,
//            IPermissionAppService permissionAppService,
//            IPasswordComplexitySettingStore passwordComplexitySettingStore)
//        {
//            _userAppService = userAppService;
//            _userManager = userManager;
//            _userLoginAppService = userLoginAppService;
//            _roleAppService = roleAppService;
//            _permissionAppService = permissionAppService;
//            _passwordComplexitySettingStore = passwordComplexitySettingStore;
//        }

//        public async Task<ActionResult> Index()
//        {
//            var roles = new List<ComboboxItemDto>();

//                var getRolesOutput = await _roleAppService.GetRoles(new GetRolesInput());
//                roles = getRolesOutput.Items.Select(r => new ComboboxItemDto(r.Id.ToString(), r.DisplayName)).ToList();

//            roles.Insert(0, new ComboboxItemDto("", ""));

//            var permissions = _permissionAppService.GetAllPermissions().Items.ToList();

//            var model = new UsersViewModel
//            {
//                FilterText = Request.Query["filterText"],
//                Roles = roles,
//                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
//                OnlyLockedUsers = false
//            };

//            return View(model);
//        }

//        public async Task<PartialViewResult> CreateOrEditModal(int? id,int tenantid)
//        {
//            var output = await _userAppService.GetUserForEdit(id, tenantid);
//            var viewModel = ObjectMapper.Map<CreateOrEditUserModalViewModel>(output);
//            viewModel.PasswordComplexitySetting = await _passwordComplexitySettingStore.GetSettingsAsync();

//            return PartialView("_CreateOrEditModal", viewModel);
//        }

//        public async Task<PartialViewResult> PermissionsModal(long id)
//        {
//            var output = await _userAppService.GetUserPermissionsForEdit(new EntityDto<long>(id));
//            var viewModel = ObjectMapper.Map<UserPermissionsEditViewModel>(output);
//            viewModel.User = await _userManager.GetUserByIdAsync(id); ;
//            return PartialView("_PermissionsModal", viewModel);
//        }

//        public async Task<PartialViewResult> LoginAttemptsModal()
//        {
//            var output = await _userLoginAppService.GetRecentUserLoginAttempts();
//            var model = new UserLoginAttemptModalViewModel
//            {
//                LoginAttempts = output.Items.ToList()
//            };
//            return PartialView("_LoginAttemptsModal", model);
//        }
//    }
//}