using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.Zero.EntityFrameworkCore;
using AbpNet8.Authorization.Users;
using AbpNet8.Editions;
using System.Threading.Tasks;
using System.Transactions;
using System;

namespace AbpNet8.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore,
            IUnitOfWorkManager unitOfWorkManager,
            IAbpZeroDbMigrator abpZeroDbMigrator) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
        }
        public async Task<int> CreateWithAdminUserAsync(
            string tenancyName,
            string name,
            string adminPassword,
            string adminEmailAddress,
            string connectionString,
            bool isActive,
            int? editionId,
            bool shouldChangePasswordOnNextLogin,
            bool sendActivationEmail,
            DateTime? subscriptionEndDate,
            bool isInTrialPeriod,
            string emailActivationLink)
        {
            int newTenantId;
            //await CheckEditionAsync(editionId, isInTrialPeriod);

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                //Create tenant
                var tenant = new Tenant(tenancyName, name)
                {
                    IsActive = isActive,
                    EditionId = editionId,
                    //SubscriptionEndDateUtc = subscriptionEndDate?.ToUniversalTime(),
                    //IsInTrialPeriod = isInTrialPeriod,
                    ConnectionString = connectionString.IsNullOrWhiteSpace() ? null : SimpleStringCipher.Instance.Encrypt(connectionString)
                };

                await CreateAsync(tenant);
                await _unitOfWorkManager.Current.SaveChangesAsync(); //To get new tenant's id.

                //_abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);
                newTenantId = tenant.Id;
                await uow.CompleteAsync();
            }

            return newTenantId;
        }
    }
}
