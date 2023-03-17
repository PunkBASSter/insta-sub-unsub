using InstaCommon;
using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;

namespace InstaCrawlerApp
{
    public class UserCrawler : JobBase
    {
        private readonly IFollowersProvider _followersProvider;
        private readonly IUserDetailsProvider _detailsProvider;
        
        public UserCrawler(IFollowersProvider followersProvider, IUserDetailsProvider detailsProvider,
            IRepository repo, ILogger<UserCrawler> logger) : base(repo, logger)
        {
            _followersProvider = followersProvider;
            _detailsProvider = detailsProvider;
            LimitPerIteration += new Random(DateTime.Now.Microsecond).Next(-82, 47); //randomizing the iteration limit
        }

        protected override int LimitPerIteration { get; set; }

        protected override async Task<JobAuditRecord> ExecuteInternal(JobAuditRecord auditRecord, CancellationToken stoppingToken)
        {   
            return await Task.Run(() =>
            {
                var crawled = Crawl();
                auditRecord.ProcessedNumber = crawled;
                auditRecord.LimitPerIteration = LimitPerIteration;
                auditRecord.AccountName = _followersProvider.LoggedInUsername ?? string.Empty;

                return auditRecord;
            });
        }

        public int Crawl()
        {
            var crawledUsersCount = 0;

            while (crawledUsersCount <= LimitPerIteration)
            {
                crawledUsersCount += CrawlFromLastUser();
                new Delay().Random();
            }

            return crawledUsersCount;
        }

        private InstaUser GetSeedUser()
        {
            var userQuery = Repository.Query<InstaUser>();
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
                Repository.Update(detailedSeedUser);
                Repository.SaveChanges();
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