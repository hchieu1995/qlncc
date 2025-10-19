using Abp.Modules;
using Abp.Reflection.Extensions;
using Admin.Localization;
using System;

namespace Admin.Core
{
    public class BnnCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            //workaround for issue: https://github.com/aspnet/EntityFrameworkCore/issues/9825
            //related github issue: https://github.com/aspnet/EntityFrameworkCore/issues/10407
            AppContext.SetSwitch("Microsoft.EntityFrameworkCore.Issue9825", true);

            // add localization source
           BnnLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BnnCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
        }
    }
}