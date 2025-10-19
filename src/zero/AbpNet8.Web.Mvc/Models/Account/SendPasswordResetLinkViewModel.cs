using System.ComponentModel.DataAnnotations;

namespace AbpNet8.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}