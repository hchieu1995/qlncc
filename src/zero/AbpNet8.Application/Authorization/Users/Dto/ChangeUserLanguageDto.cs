using System.ComponentModel.DataAnnotations;

namespace AbpNet8.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
