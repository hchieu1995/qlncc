using Abp.AspNetCore.Mvc.Authorization;
using AbpNet8.Controllers;
using Admin.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace AbpNet8.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    [Area("Admin")]
    public class CauHinhHeThongController : AbpNet8ControllerBase
    {
        private readonly QuanLyNguoiDungAppService _quanLyNguoiDungAppService;

        public CauHinhHeThongController( QuanLyNguoiDungAppService quanLyNguoiDungAppService)
        {
            _quanLyNguoiDungAppService = quanLyNguoiDungAppService;
        }

        public ActionResult Index()
        {
            //var nguoidung = _quanLyNguoiDungAppService.GetNguoiDungToChuc();

            //var tochuc = ObjectMapper.Map<List<Ql_CoCauToChucDto>>(nguoidung.ToChucCons);

            //CauHinhSoThongBaoModal viewModel = new()
            //{
            //    dsql_CoCauToChucDto = tochuc,
            //};
            //viewModel.cauHinhSoThongBao.HieuLuc = true;
            //return View(viewModel);
            return View();
        }
    }
}
