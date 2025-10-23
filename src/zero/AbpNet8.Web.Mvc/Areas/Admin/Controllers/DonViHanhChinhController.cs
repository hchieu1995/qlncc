using Abp.AspNetCore.Mvc.Authorization;
using AbpNet8.Controllers;
using Admin.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace AbpNet8.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    [Area("Admin")]
    public class DonViHanhChinhController : AbpNet8ControllerBase
    {
        private readonly DonViHanhChinhAppService _donViHanhChinhAppService;

        public DonViHanhChinhController(
            DonViHanhChinhAppService donViHanhChinhAppService)
        {
            _donViHanhChinhAppService = donViHanhChinhAppService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
