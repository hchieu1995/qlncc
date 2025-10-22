using System.ComponentModel.DataAnnotations;

namespace AbpNet8.Roles.Dto
{
    public class RoleEditDto
    {
        public int? Id { get; set; }
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