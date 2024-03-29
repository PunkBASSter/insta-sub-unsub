﻿using InstaDomain.Account;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace InstaCrawlerApp.Account
{
    /// <summary>
    /// Register as singleton.
    /// </summary>
    public sealed class AccountPool : IDisposable
    {
        private readonly IRepository _repository;
        private readonly object _lockObject = new object();
        private readonly IDictionary<string, AccountUsageHistory> _accountsInUse = new ConcurrentDictionary<string, AccountUsageHistory>();
        private readonly IServiceScope _singletonScope;

        public AccountPool(IServiceProvider sp)
        {
            _singletonScope = sp.CreateScope();
            _repository = _singletonScope.ServiceProvider.GetRequiredService<IRepository>(); //Can't pass scoped-registered IRepository directly to a singleton.
        }

        public void Dispose()
        {
            _singletonScope.Dispose();
            GC.SuppressFinalize(this);
        }

        public InstaAccount? GetLeastRecentAccount()
        {
            lock (_lockObject)
            {
                var accountsOrderedByLastUse = _repository.TrackedQuery<AccountUsageHistory>()
                    .OrderBy(auh => auh.LastUsedTime)
                    .Where(auh =>
                        auh.AntiBotDetectedTime == null || auh.AntiBotDetectedTime < DateTime.UtcNow.AddHours(-24))
                    .ToList();

                var res = accountsOrderedByLastUse.FirstOrDefault(a => !_accountsInUse.ContainsKey(a.Username));

                if (res != null)
                    _accountsInUse.Add(res.Username, res);

                return res;
            }
        }

        public void ReleaseAccountAndSaveUsageHistory(InstaAccount account, string jobName, int lastEntitiesProcessed, DateTime? antiBotDetectedTime)
        {
            lock (_lockObject)
            {
                var accountInstance = _accountsInUse[account.Username];
                _accountsInUse.Remove(account.Username);

                accountInstance.LastEntitiesProcessed = lastEntitiesProcessed;
                accountInstance.LastUsedTime = DateTime.UtcNow;
                accountInstance.LastUsedInService = jobName;
                accountInstance.AntiBotDetectedTime = antiBotDetectedTime;

                _repository.Update(accountInstance);
                _repository.SaveChanges();
            }
        }
    }
}
