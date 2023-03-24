using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaDomain.Enums;
using Microsoft.Extensions.Logging;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaCommon.Config.Jobs;
using InstaInfrastructureAbstractions;

namespace InstaCrawlerApp.Jobs
{
    /// <summary>
    /// Supposed to get a not-null account parameter in the ctor to somehow differ from its ancestor.
    /// </summary>
    public class UserFullDetailsProvider : UserQuickDetailsProvider
    {
        public UserFullDetailsProvider(IUserDetailsProvider detailsProvider, IRepository repo,
            ILogger<UserFullDetailsProvider> logger, UserFullDetailsProviderJobConfig config,
            IAccountProvider<UserFullDetailsProvider> accountProvider)
            : base(detailsProvider, repo, logger, config, accountProvider)
        {
        }

        protected override IList<InstaUser> FetchUsersToFill()
        {
            var usersToVisit = _repo.Query<InstaUser>().Where(u => u.Status == UserStatus.New
                && u.HasRussianText == true
                && u.IsClosed != true
                && u.Rank == 0
                && u.FollowingDate == null
                && u.UnfollowingDate == null
            ).Take(LimitPerIteration).ToList();

            return usersToVisit;
        }
    }
}
