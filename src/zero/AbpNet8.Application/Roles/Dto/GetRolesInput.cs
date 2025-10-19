using AbpNet8.Paging;
using System.Collections.Generic;

namespace AbpNet8.Roles.Dto
{
    public class GetRolesInput : PagedAndSortedInputDto
    {
        public string Filter { get; set; }
        public int? TenantId { get; set; }
        public List<string> Permissions { get; set; }
    }
}
