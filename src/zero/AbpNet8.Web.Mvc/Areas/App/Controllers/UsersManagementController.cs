using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbpNet8.Controllers;
using AbpNet8.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AbpNet8.Web.Areas.App.Controllers
{
    public class UsersManagementController : AbpNet8ControllerBase
    {
        public UsersManagementController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}