using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace AbpNet8.Roles.Dto
{
    public class RoleEditDto: EntityDto<int>
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Name { get; set; }
        public int? TenantId { get; set; }
        public int? DonViId { get; set; }
        public bool IsDefault { get; set; }
        public bool? IsAssigned { get; set; }
    }
}