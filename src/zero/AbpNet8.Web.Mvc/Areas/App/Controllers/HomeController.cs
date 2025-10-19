using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.MultiTenancy;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.Controllers;
using Admin.Authorization;

namespace AbpNet8.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class HomeController : AbpNet8ControllerBase
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "TraCuu");
        }
    }
}