using Abp.IdentityServer4vNext;
using Abp.Zero.EntityFrameworkCore;
using AbpNet8.Authorization.Roles;
using AbpNet8.Authorization.Users;
using AbpNet8.MultiTenancy;
using Admin.Domains;
using Microsoft.EntityFrameworkCore;

namespace Admin.EntityFrameworkCore
{
    public class BnnDbContext : AbpZeroDbContext<Tenant, Role, User, BnnDbContext>, IAbpPersistedGrantDbContext
    {
        //No Tenant
        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }
        public virtual DbSet<NguoiDung_ThongTin> NguoiDung_ThongTins { get; set; }
        public virtual DbSet<C_DonViHC> C_DonViHCs { get; set; }
        public virtual DbSet<Dm_CauHinh> Dm_CauHinhs { get; set; }
        public virtual DbSet<Dm_TinhThanh> Dm_TinhThanhs { get; set; }

        public BnnDbContext(DbContextOptions<BnnDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
