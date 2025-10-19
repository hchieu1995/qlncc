using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;

namespace AbpNet8.Roles.Dto
{
    public class RoleListDto : EntityDto, IHasCreationTime
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsStatic { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreationTime { get; set; }

        public string TenancyName { get; set; }

        public string UnitName { get; set; }

        public string UnitCode { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public int? TenantId { get; set; }
    }
}
