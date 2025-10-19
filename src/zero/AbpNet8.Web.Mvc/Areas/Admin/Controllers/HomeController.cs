using Abp.AspNetCore.Mvc.Authorization;
using AbpNet8.Authorization.Users;
using AbpNet8.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AbpNet8.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    [Area("Admin")]
    public class HomeController : AbpNet8ControllerBase
    {
        private readonly UserManager _userManager;

        public HomeController(
            UserManager userManager
         )
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            if (AbpSession.UserId.HasValue)
            {
                var nguoidung = _userManager.Users.FirstOrDefault(m => m.Id == AbpSession.UserId.Value);
                if (nguoidung != null && !string.IsNullOrEmpty(nguoidung.UserName))
                {
                    if (nguoidung.UserName.Contains("@"))
                    {
                        return RedirectToAction("Index", "HoaDonDauVao", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "QuanLyNguoiDung", new { area = "Admin" });
                    }
                }

            }
            return RedirectToAction("Login", "Account");
        }
    }
}
