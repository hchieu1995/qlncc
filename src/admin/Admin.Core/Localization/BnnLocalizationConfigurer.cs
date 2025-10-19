using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries.Xml;
using Abp.Localization.Sources;
using Abp.Reflection.Extensions;
using AbpNet8;

namespace Admin.Localization
{
    public static class BnnLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Extensions.Add(
                new LocalizationSourceExtensionInfo(
                    AbpNet8Consts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(BnnLocalizationConfigurer).GetAssembly(),
                        "Admin.Localization.Admin" // root namespace
                    )
                )
            );
        }
    }
}
