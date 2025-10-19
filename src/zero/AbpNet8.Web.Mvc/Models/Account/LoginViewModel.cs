using Abp.Auditing;
using System.ComponentModel.DataAnnotations;

namespace AbpNet8.Web.Models.Account
{
    public class LoginViewModel : LoginModel
    {
        public string MaSoThue { get; set; }
        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        [DisableAuditing]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}