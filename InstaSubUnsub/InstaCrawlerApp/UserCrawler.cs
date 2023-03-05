using InstaCommon;
using InstaDomain;
using InstaInfrastructureAbstractions.DataProviderInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;

namespace InstaCrawlerApp
{
    public class UserCrawler
    {
        private readonly InstaAccount _account;
        private readonly IRepository _repo;
        private readonly IFollowersProvider _followersProvider;
        private readonly int _crawlLimitPerIteration = 1273;

        public UserCrawler(IFollowersProvider followersProvider, IRepository repo, InstaAccount account)
        {
            _followersProvider = followersProvider;
            _repo = repo;
            _account = account;
            _crawlLimitPerIteration += new Random(DateTime.Now.Microsecond).Next(-982, 847); //randomizing the iteration limit
        }

        public void Crawl()
        {
            var crawledUsersCount = 0;

            var lastStuckUserIds = new List<long>(); //last users from which 0 new followings were saved
            while (crawledUsersCount <= _crawlLimitPerIteration)
            {
                crawledUsersCount += CrawlFromLastUser(lastStuckUserIds);
                new Delay().Random();
            }
        }

        private int CrawlFromLastUser(List<long> lastStuckUserIds)
        {
            var userQuery = _repo.Query<InstaUser>();
                        
            var seedUser = userQuery.OrderBy(u => u.Id)
                .LastOrDefault(u => (u.HasRussianText == true || u.HasRussianText == null) && !lastStuckUserIds.Contains(u.Id))
                ?? userQuery.Last(u => !lastStuckUserIds.Contains(u.Id));

            var followerItems = _followersProvider.GetByUser(seedUser, _account);

            int savedCount = 0;
            foreach (var user in followerItems)
            {
                var saved = SaveInstaUser(user, ref savedCount);
                SaveUserRelation(saved, seedUser);                
            }

            if (savedCount == 0)
                lastStuckUserIds.Add(seedUser.Id);

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