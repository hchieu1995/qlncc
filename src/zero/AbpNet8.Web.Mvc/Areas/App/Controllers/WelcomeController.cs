using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.Web.Controllers;
using AbpNet8.Controllers;

namespace AbpNet8.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize]
    public class WelcomeController : AbpNet8ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}