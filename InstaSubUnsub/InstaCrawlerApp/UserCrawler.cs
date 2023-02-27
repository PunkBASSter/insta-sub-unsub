using InstaDomain;
using InstaDomain.Enums;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using SeleniumUtils;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    public class UserCrawler
    {
        private readonly LoginPage _loginPage;
        private readonly FollowersPage _userPage;
        private readonly Account _account;
        private bool _isInitialized = false;
        private readonly IRepository _repo;
        private readonly int _crawlLimitPerIteration = 1273;

        public UserCrawler(LoginPage loginPage, FollowersPage followingPage, IRepository repo, Account account)
        {
            _loginPage = loginPage;
            _userPage = followingPage;
            _repo = repo;
            _account = account;
            _crawlLimitPerIteration += new Random(DateTime.Now.Microsecond).Next(-982, 847); //randomizing the iteration limit
        }

        /// <summary>
        /// The script signs in and goes to the main user's following page (/{username}/following).
        /// Scrolls the following page to bottom saving the users to the DB.
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized) return;

            _loginPage.Load();
            _loginPage.Login(_account.Username, _account.Password);
            _isInitialized = true;
            //todo save cookies (now or before quitting iteration?)
        }

        public void Crawl()
        {
            Initialize();

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

            var followingItems = VisitUserAndGetFollowing(seedUser.Name);
            var users = followingItems.Select(i => new InstaUser
            {
                Name = i.UserName,
                Status = UserStatus.New,
                HasRussianText = i.Description.HasRussianText() 
            }).ToList();

            int savedCount = 0;
            foreach (var user in users)
            {
                var saved = SaveInstaUser(user, ref savedCount);
                SaveUserRelation(seedUser, saved);                
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

        private void SaveUserRelation(InstaUser user, InstaUser followed)
        {
            var relation = _repo.Query<UserRelation>().FirstOrDefault(ur => ur.FollowerId == user.Id && ur.FolloweeId == followed.Id);

            if (relation is null)
            {
                var rel = new UserRelation { FollowerId = user.Id, FolloweeId = followed.Id, LastUpdate = DateTime.UtcNow };
                _repo.Insert(rel);
                _repo.SaveChanges();
                return;
            }
            
            relation.LastUpdate = DateTime.UtcNow;
            _repo.Update(relation);
            _repo.SaveChanges();
        }

        private IList<FollowingItem> VisitUserAndGetFollowing(string userName)
        {
            //basically a following page action
            _userPage.Load(userName);
            var items = _userPage.InfiniteScrollToBottomWithItemsLoading();
            return items;
        }

    }
}