using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using AbpNet8.Configuration;
using AbpNet8.Web;

namespace AbpNet8.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AbpNet8DbContextFactory : IDesignTimeDbContextFactory<AbpNet8DbContext>
    {
        public AbpNet8DbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AbpNet8DbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            AbpNet8DbContextConfigurer.Configure(builder, configuration.GetConnectionString(AbpNet8Consts.ConnectionStringName));

            return new AbpNet8DbContext(builder.Options);
        }
    }
}
