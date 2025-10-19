using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Admin.Authorization;
using Admin.Core;
using Admin.EntityFrameworkCore.EntityFrameworkCore;
using Admin.Shared;

namespace Admin.Application
{
    [DependsOn(
        typeof(BnnCoreModule),
        typeof(BnnEntityFrameworkCoreModule),
        typeof(BnnSharedModule)

       )]
    public class BnnApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();
            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BnnApplicationModule).GetAssembly());
        }
    }
}
