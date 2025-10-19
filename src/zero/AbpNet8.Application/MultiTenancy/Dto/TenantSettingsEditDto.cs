using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.MultiTenancy;
using Abp.Runtime.Validation;
using Abp.Timing;
using AbpNet8.Authorization.Users.Dto;
using AbpNet8.Configuration.Dto;
using AbpNet8.Sessions.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.MultiTenancy.Dto
{
    public class TenantSettingsEditDto
    {
        public GeneralSettingsEditDto General { get; set; }

        [Required]
        public TenantUserManagementSettingsEditDto UserManagement { get; set; }

        public TenantEmailSettingsEditDto Email { get; set; }

        public LdapSettingsEditDto Ldap { get; set; }

        [Required]
        public SecuritySettingsEditDto Security { get; set; }

        public TenantBillingSettingsEditDto Billing { get; set; }

        public TenantOtherSettingsEditDto OtherSettings { get; set; }

        /// <summary>
        /// This validation is done for single-tenant applications.
        /// Because, these settings can only be set by tenant in a single-tenant application.
        /// </summary>
        public void ValidateHostSettings()
        {
            var validationErrors = new List<ValidationResult>();
            if (Clock.SupportsMultipleTimezone && General == null)
            {
                validationErrors.Add(new ValidationResult("General settings can not be null", new[] { "General" }));
            }

            if (Email == null)
            {
                validationErrors.Add(new ValidationResult("Email settings can not be null", new[] { "Email" }));
            }

            if (validationErrors.Count > 0)
            {
                throw new AbpValidationException("Method arguments are not valid! See ValidationErrors for details.", validationErrors);
            }
        }
    }
    public class LdapSettingsEditDto
    {
        public bool IsModuleEnabled { get; set; }

        public bool IsEnabled { get; set; }

        public string Domain { get; set; }

        public string UserName { get; set; }

        [DisableAuditing]
        public string Password { get; set; }
    }
    public class TenantBillingSettingsEditDto
    {
        public string LegalName { get; set; }
        public string Address { get; set; }
        public string TaxVatNo { get; set; }
    }
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
    public class TenantOtherSettingsEditDto
    {
        public bool IsQuickThemeSelectEnabled { get; set; }
    }
    public class TenantUserManagementSettingsEditDto
    {
        public bool AllowSelfRegistration { get; set; }

        public bool IsNewRegisteredUserActiveByDefault { get; set; }

        public bool IsEmailConfirmationRequiredForLogin { get; set; }

        public bool UseCaptchaOnRegistration { get; set; }

        public bool UseCaptchaOnLogin { get; set; }

        public bool IsCookieConsentEnabled { get; set; }

        public bool IsQuickThemeSelectEnabled { get; set; }

        public SessionTimeOutSettingsEditDto SessionTimeOutSettings { get; set; }
    }
    public class GeneralSettingsEditDto
    {
        public string Timezone { get; set; }

        /// <summary>
        /// This value is only used for comparing user's timezone to default timezone
        /// </summary>
        public string TimezoneForComparison { get; set; }
    }
    public class SecuritySettingsEditDto
    {
        public bool AllowOneConcurrentLoginPerUser { get; set; }

        public bool UseDefaultPasswordComplexitySettings { get; set; }

        public PasswordComplexitySetting PasswordComplexity { get; set; }

        public PasswordComplexitySetting DefaultPasswordComplexity { get; set; }

        public UserLockOutSettingsEditDto UserLockOut { get; set; }

        public TwoFactorLoginSettingsEditDto TwoFactorLogin { get; set; }
    }
    public class UserLockOutSettingsEditDto
    {
        public bool IsEnabled { get; set; }

        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }

        public int DefaultAccountLockoutSeconds { get; set; }
    }
    public class TwoFactorLoginSettingsEditDto
    {
        public bool IsEnabledForApplication { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsEmailProviderEnabled { get; set; }

        public bool IsSmsProviderEnabled { get; set; }

        public bool IsRememberBrowserEnabled { get; set; }

        public bool IsGoogleAuthenticatorEnabled { get; set; }
    }
    public class TenantEditDto : EntityDto
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(TenantConsts.MaxNameLength)]
        public string Name { get; set; }

        [DisableAuditing]
        public string ConnectionString { get; set; }

        public int? EditionId { get; set; }

        public bool IsActive { get; set; }

        public DateTime? SubscriptionEndDateUtc { get; set; }

        public bool IsInTrialPeriod { get; set; }
    }
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
    public class FlatFeatureDto
    {
        public string ParentName { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string DefaultValue { get; set; }

        public FeatureInputTypeDto InputType { get; set; }
    }
    public class FeatureInputTypeDto
    {
        public string Name { get; set; }

        public IDictionary<string, object> Attributes { get; set; }

        public IValueValidator Validator { get; set; }

        public LocalizableComboboxItemSourceDto ItemSource { get; set; }
    }
    public class LocalizableComboboxItemSourceDto
    {
        public Collection<LocalizableComboboxItemDto> Items { get; set; }
    }
    public class LocalizableComboboxItemDto
    {
        public string Value { get; set; }

        public string DisplayText { get; set; }
    }
}
