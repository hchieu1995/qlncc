using System.ComponentModel.DataAnnotations;

namespace AbpNet8.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}