//using System.Threading.Tasks;
//using Abp.AspNetCore.Mvc.Authorization;
//using Abp.Auditing;
//using Microsoft.AspNetCore.Mvc;
//using AbpNet8.Auditing;
//using AbpNet8.Auditing.Dto;
//using AbpNet8.Web.Areas.App.Models.AuditLogs;
//using AbpNet8.Web.Controllers;
//using HopDong.Core.Authorization;
//using AbpNet8.Controllers;

//namespace AbpNet8.Web.Areas.App.Controllers
//{
//    [Area("App")]
//    [DisableAuditing]
//    [AbpMvcAuthorize(AppPermissions.Pages)]
//    public class AuditLogsController : AbpNet8ControllerBase
//    {
//        private readonly IAuditLogAppService _auditLogAppService;

//        public AuditLogsController(IAuditLogAppService auditLogAppService)
//        {
//            _auditLogAppService = auditLogAppService;
//        }

//        public ActionResult Index()
//        {
//            return View();
//        }

//        public async Task<PartialViewResult> EntityChangeDetailModal(EntityChangeListDto entityChangeListDto)
//        {
//            var output = await _auditLogAppService.GetEntityPropertyChanges(entityChangeListDto.Id);

//            var viewModel = new EntityChangeDetailModalViewModel(output, entityChangeListDto);

//            return PartialView("_EntityChangeDetailModal", viewModel);
//        }
//    }
//}