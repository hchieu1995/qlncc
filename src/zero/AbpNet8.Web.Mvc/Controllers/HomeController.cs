using System;
using System.Threading.Tasks;
using Abp;
using Abp.Localization;
using Abp.Notifications;
using Microsoft.AspNetCore.Mvc;
using AbpNet8.Identity;
using AbpNet8.Controllers;
using AbpNet8.Authorization.Users;
using System.Linq;

namespace AbpNet8.Web.Controllers
{
    public class HomeController : AbpNet8ControllerBase
    {
        private readonly SignInManager _signInManager;
        private readonly UserManager _userManager;

        public HomeController(
            SignInManager signInManager,
            UserManager userManager

         )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string redirect = "", bool forceNewRegistration = false)
        {
            if (forceNewRegistration)
            {
                await _signInManager.SignOutAsync();
            }

            if (redirect == "TenantRegistration")
            {
                return RedirectToAction("SelectEdition", "TenantRegistration");
            }
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