using InstaCommon;
using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;

namespace InstaCrawlerApp
{
    public class UserCrawler
    {
        private readonly IRepository _repo;
        private readonly IFollowersProvider _followersProvider;
        private readonly IUserDetailsProvider _detailsProvider;
        private readonly int _crawlLimitPerIteration = 973;

        public UserCrawler(IFollowersProvider followersProvider, IUserDetailsProvider detailsProvider, IRepository repo)
        {
            _followersProvider = followersProvider;
            _detailsProvider = detailsProvider;
            _repo = repo;
            _crawlLimitPerIteration += new Random(DateTime.Now.Microsecond).Next(-82, 47); //randomizing the iteration limit
        }

        public void Crawl()
        {
            var crawledUsersCount = 0;

            while (crawledUsersCount <= _crawlLimitPerIteration)
            {
                crawledUsersCount += CrawlFromLastUser();
                new Delay().Random();
            }
        }

        private InstaUser GetSeedUser()
        {
            var userQuery = _repo.Query<InstaUser>();
            InstaUser? seedUser;
            var attempts = 10;
            do
            {
                seedUser = userQuery.Where(u =>
                u.HasRussianText == true
                    && u.IsClosed != true
                    && u.Rank >= 3
                    && u.LastPostDate >= DateTime.UtcNow.AddDays(-15))
                    .OrderBy(u => u.Id)
                    .LastOrDefault();

                //softer criteria
                seedUser ??= userQuery.Where(u => (u.HasRussianText == true) && (u.IsClosed != true))
                    .OrderBy(u => u.Id)
                    .LastOrDefault();
                    if (seedUser == null)
                        throw new InvalidOperationException("FATAL: Could not find any suitable user to start crawling. Probably database is empty.");

                var detailedSeedUser = _detailsProvider.GetUserDetails(seedUser);
                _repo.Update(detailedSeedUser);
                _repo.SaveChanges();
                seedUser = detailedSeedUser;
                attempts--;
            }
            while ((seedUser.IsClosed == true || seedUser.FollowersNum == 0) && attempts > 0);
            
            return seedUser;
        }

        private int CrawlFromLastUser()
        {
            var detailedSeedUser = GetSeedUser();

            _followersProvider.LoggedInUsername = _detailsProvider.LoggedInUsername; //wierd way to transfer state between infrastructure services
            var followerItems = _followersProvider.GetByUser(detailedSeedUser);

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
            var savedEntity = _repo.Query<InstaUser>().FirstOrDefault(u => u.Name == user.Name);
            if (savedEntity is null)
            {
                _repo.Insert(user);
                _repo.SaveChanges();
                savedCount++;
                return user;
            }
            
            //skip if exists
            return savedEntity;
        }

        private void SaveUserRelation(InstaUser follower, InstaUser followed)
        {
            var relation = _repo.Query<UserRelation>().FirstOrDefault(ur => ur.FollowerId == follower.Id && ur.FolloweeId == followed.Id);

            if (relation is null)
            {
                var rel = new UserRelation { FollowerId = follower.Id, FolloweeId = followed.Id, LastUpdate = DateTime.UtcNow };
                _repo.Insert(rel);
                _repo.SaveChanges();
                return;
            }
            
            relation.LastUpdate = DateTime.UtcNow;
            _repo.Update(relation);
            _repo.SaveChanges();
        }
    }
}