
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Admin.EntityFrameworkCore.EntityFrameworkCore
{
    public static class BnnDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BnnDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString).
                EnableSensitiveDataLogging();
        }

        public static void Configure(DbContextOptionsBuilder<BnnDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection).EnableSensitiveDataLogging();
        }

        public static void Configure(DbContextOptionsBuilder<BnnTenantDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString).
                EnableSensitiveDataLogging();
        }

        public static void Configure(DbContextOptionsBuilder<BnnTenantDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection).EnableSensitiveDataLogging();
        }
    }
}
