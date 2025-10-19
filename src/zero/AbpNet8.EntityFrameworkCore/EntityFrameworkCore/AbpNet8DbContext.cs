using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AbpNet8.Authorization.Roles;
using AbpNet8.Authorization.Users;
using AbpNet8.MultiTenancy;
using AbpNet8.Storage;
using Abp.IdentityServer4vNext;

namespace AbpNet8.EntityFrameworkCore
{
    public class AbpNet8DbContext : AbpZeroDbContext<Tenant, Role, User, AbpNet8DbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        //public virtual DbSet<Friendship> Friendships { get; set; }

        //public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        //public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        //public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        //public virtual DbSet<MultiTenancy.Accounting.Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        //public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        //public virtual DbSet<Sc_Test> Tests { get; set; }

        public AbpNet8DbContext(DbContextOptions<AbpNet8DbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            //modelBuilder.Entity<ChatMessage>(b =>
            //{
            //    b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
            //    b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
            //    b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
            //    b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            //});

            //modelBuilder.Entity<Friendship>(b =>
            //{
            //    b.HasIndex(e => new { e.TenantId, e.UserId });
            //    b.HasIndex(e => new { e.TenantId, e.FriendUserId });
            //    b.HasIndex(e => new { e.FriendTenantId, e.UserId });
            //    b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            //});

            //modelBuilder.Entity<Tenant>(b =>
            //{
            //    b.HasIndex(e => new { e.SubscriptionEndDateUtc });
            //    b.HasIndex(e => new { e.CreationTime });
            //});

            //modelBuilder.Entity<SubscriptionPayment>(b =>
            //{
            //    b.HasIndex(e => new { e.Status, e.CreationTime });
            //    b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            //});

            //modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            //{
            //    b.HasQueryFilter(m => !m.IsDeleted)
            //        .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
            //        .IsUnique();
            //});

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
