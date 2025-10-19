using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Uow;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc;

namespace AbpNet8.Web.Views.Shared.Components.AccountLanguages
{
    public class AccountLanguagesViewComponent : AbpNet8ViewComponent
    {
        private readonly ILanguageManager _languageManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public AccountLanguagesViewComponent(ILanguageManager languageManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _languageManager = languageManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            using (var ouw = _unitOfWorkManager.Begin())
            {
                var model = new LanguageSelectionViewModel
                {
                    CurrentLanguage = _languageManager.CurrentLanguage,
                    Languages = _languageManager.GetActiveLanguages().ToList(),
                    CurrentUrl = Request.Path
                };
                ouw.Complete();

                return Task.FromResult(View(model) as IViewComponentResult);
            }
        }
    }
}
