using Abp.Authorization;
using AbpNet8.Authorization.Roles;
using AbpNet8.Authorization.Users;

namespace AbpNet8.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
