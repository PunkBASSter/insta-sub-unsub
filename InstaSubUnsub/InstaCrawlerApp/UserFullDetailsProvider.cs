using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaDomain.Enums;
using Microsoft.Extensions.Logging;
using InstaInfrastructureAbstractions.DataProviderInterfaces;

namespace InstaCrawlerApp
{
    /// <summary>
    /// Supposed to get a not-null account parameter in the ctor to somehow differ from its ancestor.
    /// </summary>
    public class UserFullDetailsProvider : UserQuickDetailsProvider
    {
        public UserFullDetailsProvider(IUserDetailsProvider detailsProvider, IRepository repo, InstaAccount account, ILogger<UserFullDetailsProvider> logger)
            : base(detailsProvider, repo, logger, account)
        {
        }

        protected override IList<InstaUser> FetchUsersToFill()
        {
            var usersToVisit = _repo.Query<InstaUser>().Where(u => u.Status == UserStatus.New && u.HasRussianText == true && (u.Rank == default || u.Rank > -1)).Take(_batchSize).ToList();

            return usersToVisit;
        }
    }
}
