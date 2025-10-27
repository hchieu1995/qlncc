using Abp.AspNetCore.Mvc.Authorization;
using AbpNet8.Controllers;
using Admin.AppServices;
using Admin.Model;
using Microsoft.AspNetCore.Mvc;

namespace AbpNet8.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    [Area("Admin")]
    public class QuanLyCoCauToChucController : AbpNet8ControllerBase
    {
        private readonly QuanLyCoCauToChucAppService _quanLyCoCauToChucAppService;

        public QuanLyCoCauToChucController(
            QuanLyCoCauToChucAppService quanLyCoCauToChucAppService
        )
        {
            _quanLyCoCauToChucAppService = quanLyCoCauToChucAppService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public PartialViewResult CreateNguoiDungModal(int id)
        {
            CreateOrEditToChucModalViewModel viewModel = new();
            viewModel.ToChuc = _quanLyCoCauToChucAppService.ToChucById(id);
            return PartialView("_CreateNguoiDungModal", viewModel);
        }
    }
}
