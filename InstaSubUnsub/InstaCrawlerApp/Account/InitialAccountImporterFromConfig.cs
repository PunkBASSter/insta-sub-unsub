using InstaDomain.Account;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;

namespace InstaCrawlerApp.Account
{
    public class InitialAccountImporterFromConfig
    {
        private readonly IRepository _repo;
        private readonly string _sectionName = "ServiceAccountsToImport";
        private readonly IEnumerable<AccountUsageHistory> _instaAccounts = new List<AccountUsageHistory>();
        public InitialAccountImporterFromConfig(IRepository repo, IConfiguration conf)
        {
            _repo = repo;
            conf.GetSection(_sectionName).Bind(_instaAccounts);
        }

        public void ImportAccountFromConfigToDbPool()
        {
            foreach (var account in _instaAccounts)
            {
                _repo.InsertOrSkip(account, acc => acc.Username == account.Username);
            }
            _repo.SaveChanges();
        }
    }
}
