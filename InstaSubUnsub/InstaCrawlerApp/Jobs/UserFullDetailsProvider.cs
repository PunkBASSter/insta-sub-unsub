using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaDomain.Enums;
using Microsoft.Extensions.Logging;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Account.Interfaces;

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
            var query = _repo.Query<InstaUser>();

            //First take all with recent posts (stories detected by crawler)
            var usersToVisit = query.Where(u => u.Status == UserStatus.New
                && u.Rank == 0
                && u.IsClosed != true
                && u.LastPostDate >= DateTime.UtcNow.Date.AddDays(-7)
                && u.FollowingDate == null
                && u.UnfollowingDate == null
            ).Take(LimitPerIteration).ToList();

            var leftForLimit = LimitPerIteration - usersToVisit.Count;

            if (leftForLimit == 0)
                return usersToVisit;

            //Then take the rest with known Russian text in descriotion (found by crawler)
            var usersLeftToVisitRus = query.Where(u => u.Status == UserStatus.New
                && u.Rank == 0
                && u.HasRussianText == true
                && u.IsClosed != true
                && u.LastPostDate == null
                && u.FollowingDate == null
                && u.UnfollowingDate == null
            ).Take(leftForLimit).ToList();

            leftForLimit -= usersLeftToVisitRus.Count;
            usersToVisit.AddRange(usersLeftToVisitRus);
            if (leftForLimit == 0)
                return usersToVisit; 

            //Take the rest without any known info
            var usersLeftToVisit = query.Where(u => u.Status == UserStatus.New
                && u.Rank == 0
                && u.HasRussianText == null
                && u.IsClosed != true
                && u.LastPostDate == null
                && u.FollowingDate == null
                && u.UnfollowingDate == null
            ).Take(leftForLimit).ToList();

            usersToVisit.AddRange(usersLeftToVisit);

            return usersToVisit.Distinct().ToList();
        }
    }
}
