﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using AbpNet8.Sessions.Dto;
using AbpNet8.UiCustomization;
using Microsoft.EntityFrameworkCore;

namespace AbpNet8.Sessions
{
    public class SessionAppService : AbpNet8AppServiceBase, ISessionAppService
    {
        private readonly IUiThemeCustomizerFactory _uiThemeCustomizerFactory;

        public SessionAppService(
            IUiThemeCustomizerFactory uiThemeCustomizerFactory
            )
        {
            _uiThemeCustomizerFactory = uiThemeCustomizerFactory;
        }

        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>(),
                    Currency = AbpNet8Consts.Currency,
                    CurrencySign = AbpNet8Consts.CurrencySign,
                    AllowTenantsToChangeEmailSettings = AbpNet8Consts.AllowTenantsToChangeEmailSettings
                }
            };

            var uiCustomizer = await _uiThemeCustomizerFactory.GetCurrentUiCustomizer();
            output.Theme = await uiCustomizer.GetUiSettings();
            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper
                    .Map<TenantLoginInfoDto>(await TenantManager
                        .Tenants
                        //.Include(t => t.Edition)
                        .FirstAsync(t => t.Id == AbpSession.GetTenantId()));
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            if (output.Tenant == null)
            {
                return output;
            }

            //if (output.Tenant.Edition != null)
            //{
            //    var lastPayment = await _subscriptionPaymentRepository.GetLastCompletedPaymentOrDefaultAsync(output.Tenant.Id, null, null);
            //    if (lastPayment != null)
            //    {
            //        output.Tenant.Edition.IsHighestEdition = IsEditionHighest(output.Tenant.Edition.Id, lastPayment.GetPaymentPeriodType());
            //    }
            //}

            //output.Tenant.SubscriptionDateString = GetTenantSubscriptionDateString(output);
            output.Tenant.CreationTimeString = output.Tenant.CreationTime.ToString("d");

            return output;
        }
        public async Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken()
        {
            if (AbpSession.UserId <= 0)
            {
                throw new Exception(L("ThereIsNoLoggedInUser"));
            }

            var user = await UserManager.GetUserAsync(AbpSession.ToUserIdentifier());
            user.SetSignInToken();
            return new UpdateUserSignInTokenOutput
            {
                SignInToken = user.SignInToken,
                EncodedUserId = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id.ToString())),
                EncodedTenantId = user.TenantId.HasValue
                    ? Convert.ToBase64String(Encoding.UTF8.GetBytes(user.TenantId.Value.ToString()))
                    : ""
            };
        }
    }
}
