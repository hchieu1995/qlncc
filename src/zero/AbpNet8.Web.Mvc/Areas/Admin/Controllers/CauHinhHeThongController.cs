using Abp.AspNetCore.Mvc.Authorization;
using BNNSoft.Admin.AppServices;
using BNNSoft.Admin.DomainTranferObjects.DTO;
using BNNSoft.Admin.IAppService;
using BNNSoft.WebApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BNNSoft.WebApp.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    [Area("Admin")]
    public class CauHinhHeThongController : WebAppControllerBase
    {
        private readonly IQuanLyNguoiDungAppService _quanLyNguoiDungAppService;

        public CauHinhHeThongController( IQuanLyNguoiDungAppService quanLyNguoiDungAppService)
        {
            _quanLyNguoiDungAppService = quanLyNguoiDungAppService;
        }

        public ActionResult Index()
        {
            var nguoidung = _quanLyNguoiDungAppService.GetNguoiDungToChuc();

            var tochuc = ObjectMapper.Map<List<Ql_CoCauToChucDto>>(nguoidung.ToChucCons);

            CauHinhSoThongBaoModal viewModel = new()
            {
                dsql_CoCauToChucDto = tochuc,
            };
            viewModel.cauHinhSoThongBao.HieuLuc = true;
            return View(viewModel);
        }
    }
}
