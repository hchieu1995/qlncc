using Abp.Domain.Entities.Auditing;
using AbpNet8.Roles.Dto;
using System.Collections.Generic;

namespace Admin.DomainTranferObjects.DTO
{
    public class NguoiDung_ThongTinDto : FullAuditedEntity<long>
    {
        public long UserId { get; set; }
        public string DoanhNghiep_Mst { get; set; }
        public string DoanhNghiep_Ten { get; set; }
        public string NguoiDung_TaiKhoan { get; set; }
        public string NguoiDung_HoTen { get; set; }
        public string NguoiDung_Sdt { get; set; }
        public string NguoiDung_Email { get; set; }
        public string NguoiDung_MatKhau { get; set; }
        public bool? NguoiDung_TrangThai { get; set; }
        public bool? ShouldChangePasswordOnNextLogin { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string NguoiDung_ChucVu { get; set; }
        public int? NguoiDung_GioiTinh { get; set; }
        public string NguoiDung_AnhDD { get; set; }
        public List<RoleEditDto> ListRole { get; set; }
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
