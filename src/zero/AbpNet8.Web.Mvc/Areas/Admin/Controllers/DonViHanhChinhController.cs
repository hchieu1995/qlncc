using Abp.AspNetCore.Mvc.Authorization;
using AbpNet8.Controllers;
using Admin.AppServices;
using Admin.Domains;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public PartialViewResult CreateOrEditModal(int? id, int? idCha)
        {
            C_DonViHC viewModel = new();
            if (id.HasValue)
            {
                var output = _donViHanhChinhAppService.GetDonViHanhChinhById(id.Value);
                viewModel = output;
            }
            else
            {
                viewModel = new C_DonViHC
                {
                    IsUpdate = true,
                    IdCha = idCha
                };
            }

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public PartialViewResult ChildUnitsModal(int maHC)
        {
            return PartialView("_ChildUnitsModal", maHC);
        }
    }
}
