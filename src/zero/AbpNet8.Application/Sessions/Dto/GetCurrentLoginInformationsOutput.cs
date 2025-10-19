using AbpNet8.UiCustomization.Dto;

namespace AbpNet8.Sessions.Dto
{
    public class GetCurrentLoginInformationsOutput
    {
        public ApplicationInfoDto Application { get; set; }

        public UserLoginInfoDto User { get; set; }

        public TenantLoginInfoDto Tenant { get; set; }
        public UiCustomizationSettingsDto Theme { get; set; }
    }
}
