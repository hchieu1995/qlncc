using Abp.AspNetCore.Mvc.Authorization;
using AbpNet8.Controllers;
using Admin.AppServices;
using Admin.DomainTranferObjects.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AbpNet8.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    [Area("Admin")]
    public class QuanLyNguoiDungController : AbpNet8ControllerBase
    {
        private readonly QuanLyNguoiDungAppService _quanLyNguoiDungAppService;
        public QuanLyNguoiDungController(
            QuanLyNguoiDungAppService quanLyNguoiDungAppService
        )
        {
            _quanLyNguoiDungAppService = quanLyNguoiDungAppService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult CreateOrEditModal(int? id)
        {
            NguoiDung_ThongTinDto viewModel;
            if (id.HasValue)
            {
                viewModel = _quanLyNguoiDungAppService.GetNguoiDungById(id);
            }
            else
            {
                viewModel = _quanLyNguoiDungAppService.GetNewNguoiDung();
            }

            return PartialView("_CreateOrEditModal", viewModel);
        }
        public PartialViewResult GetRoleByTenant(int? id, int? tenantid)
        {
            return PartialView("_CreateOrEditModal");
        }
        public PartialViewResult ChangePasswordModal(int userId, int? tenantId)
        {
            var viewModel = new ChangePasswordViewModel
            {
                UserId = userId,
                TenantId = tenantId
            };

            return PartialView("_ChangePasswordModal", viewModel);
        }
    }
}
