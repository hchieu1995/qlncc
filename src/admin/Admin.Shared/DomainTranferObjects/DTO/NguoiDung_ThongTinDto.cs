using AbpNet8.Roles.Dto;
using Admin.Domains;
using System.Collections.Generic;

namespace Admin.DomainTranferObjects.DTO
{
    public class NguoiDung_ThongTinDto : NguoiDung_ThongTin
    {
        public bool? ShouldChangePasswordOnNextLogin { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public List<RoleEditDto> ListRole { get; set; }
        public List<C_DonViHC> donvis { get; set; }
    }
    public class CreateEditNguoiDungThongTin
    {
        public NguoiDung_ThongTinDto NguoiDungThongTinDto { get; set; }
        public string[] AssignedRoleNames { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public int? TenantId { get; set; }
        public int UserId { get; set; }
    }
}
