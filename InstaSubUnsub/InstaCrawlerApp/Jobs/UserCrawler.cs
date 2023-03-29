using InstaCommon;
using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Account.Interfaces;
using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;

namespace InstaCrawlerApp.Jobs
{
    public class UserCrawler : JobBase
    {
        private readonly IFollowersProvider _followersProvider;
        private readonly IUserDetailsProvider _detailsProvider;

        private IEnumerable<InstaUser>? _seedUsers;
        private IEnumerator<InstaUser>? _seedUsersEnumerator;

        public UserCrawler(IFollowersProvider followersProvider, IUserDetailsProvider detailsProvider,
            IRepository repo, ILogger<UserCrawler> logger, UserCrawlerJobConfig config, IAccountProvider<UserCrawler> accProvider)
            : base(repo, logger, config, accProvider)
        {
            _followersProvider = followersProvider;
            _detailsProvider = detailsProvider;
        }

        protected override int ExecuteInternal()
        {
            var crawledUsersCount = 0;

            while (crawledUsersCount <= LimitPerIteration)
            {
                crawledUsersCount += CrawlFromLastUser();
                new Delay().Random();
            }

            return crawledUsersCount;
        }

        private InstaUser? EnumerateUsers(IEnumerable<InstaUser> users)
        {
            _seedUsersEnumerator ??= users.GetEnumerator();
            if (!_seedUsersEnumerator.MoveNext())
                return null;
            return _seedUsersEnumerator.Current;
        }

        private InstaUser GetSeedUser()
        {
            var userQuery = Repository.Query<InstaUser>();
            
            _seedUsers ??= userQuery.Where(u =>
            u.HasRussianText == true
                && u.IsClosed != true
                && u.Rank >= 3
                && u.LastPostDate >= DateTime.UtcNow.AddDays(-15))
                .OrderByDescending(u => u.Id).Take(100)
                .ToList();

            //softer criteria
            _seedUsers ??= userQuery.Where(u => u.HasRussianText == true && u.IsClosed != true)
                .OrderByDescending(u => u.Id).Take(100)
                .ToList();

            InstaUser? seedUser = null;
            seedUser = EnumerateUsers(_seedUsers);
            if (seedUser == null)
                throw new InvalidOperationException("FATAL: Could not find any suitable user to start crawling. Probably database is empty.");

            if (seedUser.Status == InstaDomain.Enums.UserStatus.New)
            {
                var detailedSeedUser = _detailsProvider.GetUserDetails(seedUser, Account);
                Repository.Update(detailedSeedUser);
                Repository.SaveChanges();
                seedUser = detailedSeedUser;
            }

            return seedUser;
        }

        private int CrawlFromLastUser()
        {
            var detailedSeedUser = GetSeedUser();

            _followersProvider.LoggedInUsername = _detailsProvider.LoggedInUsername; //wierd way to transfer state between infrastructure services
            var followerItems = _followersProvider.GetByUser(detailedSeedUser, Account);

            int savedCount = 0;
            foreach (var user in followerItems)
            {
                var saved = SaveInstaUser(user, ref savedCount);
                SaveUserRelation(saved, detailedSeedUser);
            }

            return savedCount;
        }

        private InstaUser SaveInstaUser(InstaUser user, ref int savedCount)
        {
            var savedEntity = Repository.Query<InstaUser>().FirstOrDefault(u => u.Name == user.Name);
            if (savedEntity is null)
            {
                Repository.Insert(user);
                Repository.SaveChanges();
                savedCount++;
                return user;
            }

            //skip if exists
            return savedEntity;
        }

        private void SaveUserRelation(InstaUser follower, InstaUser followed)
        {
            var relation = Repository.Query<UserRelation>().FirstOrDefault(ur => ur.FollowerId == follower.Id && ur.FolloweeId == followed.Id);

            if (relation is null)
            {
                var rel = new UserRelation { FollowerId = follower.Id, FolloweeId = followed.Id, LastUpdate = DateTime.UtcNow };
                Repository.Insert(rel);
                Repository.SaveChanges();
                return;
            }

            relation.LastUpdate = DateTime.UtcNow;
            Repository.Update(relation);
            Repository.SaveChanges();
        }

    }
}