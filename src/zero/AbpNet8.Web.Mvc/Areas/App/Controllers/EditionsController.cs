//using System.Linq;
//using System.Threading.Tasks;
//using Abp.Application.Services.Dto;
//using Abp.AspNetCore.Mvc.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.Editions;
//using AbpNet8.MultiTenancy;
//using AbpNet8.Web.Areas.App.Models.Editions;
//using AbpNet8.Web.Controllers;
//using HopDong.Core.Authorization;
//using AbpNet8.Controllers;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    [AbpMvcAuthorize(AppPermissions.Pages)]
//    public class EditionsController : AbpNet8ControllerBase
//    {
//        private readonly IEditionAppService _editionAppService;
//        private readonly TenantManager _tenantManager;

//        public EditionsController(
//            IEditionAppService editionAppService, 
//            TenantManager tenantManager)
//        {
//            _editionAppService = editionAppService;
//            _tenantManager = tenantManager;
//        }

//        public ActionResult Index()
//        {
//            return View();
//        }

//        public async Task<PartialViewResult> CreateModal(int? id)
//        {
//            var output = await _editionAppService.GetEditionForEdit(new NullableIdDto { Id = id });
//            var viewModel = ObjectMapper.Map<CreateEditionModalViewModel>(output);
//            viewModel.EditionItems = await _editionAppService.GetEditionComboboxItems(); ;
//            viewModel.FreeEditionItems = await _editionAppService.GetEditionComboboxItems(output.Edition.ExpiringEditionId, false, true); ;
  
//            return PartialView("_CreateModal", viewModel);
//        }

//        public async Task<PartialViewResult> EditModal(int? id)
//        {
//            var output = await _editionAppService.GetEditionForEdit(new NullableIdDto { Id = id });
//            var viewModel = ObjectMapper.Map<EditEditionModalViewModel>(output);
//            viewModel.EditionItems = await _editionAppService.GetEditionComboboxItems(); ;
//            viewModel.FreeEditionItems = await _editionAppService.GetEditionComboboxItems(output.Edition.ExpiringEditionId, false, true); ;

//            return PartialView("_EditModal", viewModel);
//        }

//        public async Task<PartialViewResult> MoveTenantsToAnotherEdition(int id)
//        {
//            var editionItems = await _editionAppService.GetEditionComboboxItems();
//            var tenantCount = _tenantManager.Tenants.Count(t => t.EditionId == id);

//            var viewModel = new MoveTenantsToAnotherEditionViewModel
//            {
//                EditionId = id,
//                TenantCount = tenantCount,
//                EditionItems = editionItems
//            };

//            return PartialView("_MoveTenantsToAnotherEdition", viewModel);
//        }
//    }
//}