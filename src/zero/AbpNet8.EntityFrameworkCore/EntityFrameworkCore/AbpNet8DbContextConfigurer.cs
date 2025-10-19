using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AbpNet8.EntityFrameworkCore
{
    public static class AbpNet8DbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AbpNet8DbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString).EnableSensitiveDataLogging();
        }

        public static void Configure(DbContextOptionsBuilder<AbpNet8DbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection).EnableSensitiveDataLogging();
        }
    }
}
