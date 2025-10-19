using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpNet8.Authorization;

namespace AbpNet8
{
    [DependsOn(
        typeof(AbpNet8CoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AbpNet8ApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<AbpNet8AuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AbpNet8ApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
