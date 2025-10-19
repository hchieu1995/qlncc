using Abp.AspNetCore.Mvc.ViewComponents;

namespace AbpNet8.Web.Views
{
    public abstract class AbpNet8ViewComponent : AbpViewComponent
    {
        protected AbpNet8ViewComponent()
        {
            LocalizationSourceName = AbpNet8Consts.LocalizationSourceName;
        }
    }
}