using AbpNet8;
using AbpNet8.Configuration;
using AbpNet8.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Admin.EntityFrameworkCore.EntityFrameworkCore
{
    public class BnnDbContextFactory : IDesignTimeDbContextFactory<BnnDbContext>
    {
        public BnnDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BnnDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            BnnDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AbpNet8Consts.ConnectionStringName));

            return new BnnDbContext(builder.Options);
        }
    }
}
