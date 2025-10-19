using Abp.Modules;
using Abp.Reflection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Shared
{
    public class BnnSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            //IocManager.Register<IExcelHelper, ExcelHelper>(DependencyLifeStyle.Transient);
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BnnSharedModule).GetAssembly());
        }
    }
}
