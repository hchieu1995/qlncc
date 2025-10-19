using System.Threading.Tasks;
using AbpNet8.Configuration.Dto;

namespace AbpNet8.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
