using Abp.IdentityServer4vNext;
using Abp.Zero.EntityFrameworkCore;
using AbpNet8.Authorization.Roles;
using AbpNet8.Authorization.Users;
using Microsoft.EntityFrameworkCore;

namespace Admin.EntityFrameworkCore.EntityFrameworkCore
{
    public class BnnTenantDbContext : AbpZeroTenantDbContext<Role, User, BnnTenantDbContext>, IAbpPersistedGrantDbContext
    {
        //public virtual DbSet<Sc_Test> Tests { get; set; }
        public BnnTenantDbContext(DbContextOptions<BnnTenantDbContext> options) : base(options)
        {
        }

        public DbSet<PersistedGrantEntity> PersistedGrants { get; set; }
    }
}
