using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using AbpNet8.Configuration.Dto;

namespace AbpNet8.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : AbpNet8AppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
