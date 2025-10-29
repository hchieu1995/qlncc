using Abp.AspNetCore.Mvc.Authorization;
using AbpNet8.Controllers;
using Admin.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace AbpNet8.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    [Area("Admin")]
    public class DanhMucTinhThanhController : AbpNet8ControllerBase
    {
        private readonly DanhMucTinhThanhAppService _dmTinhThanhAppService;

        public DanhMucTinhThanhController(
            DanhMucTinhThanhAppService dmTinhThanhAppService)
        {
            _dmTinhThanhAppService = dmTinhThanhAppService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
