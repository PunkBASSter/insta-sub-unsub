using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using SeleniumUtils.PageObjects;
using InstaDomain.Enums;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace InstaCrawlerApp
{
    public class UserFullDetailsProvider : UserQuickDetailsProvider
    {
        private readonly Account _account;
        private bool _isInitialized = false;
        
        public UserFullDetailsProvider(IWebDriver driver, IRepository repo, Account account, ILogger<UserFullDetailsProvider> logger)
            : base(driver, repo, logger)
        {
            _account = account;
        }

        protected override bool Initialize()
        {
            if (_isInitialized) return true;

            var loginPage = new LoginPage(_webDriver);
            loginPage.Load();
            loginPage.Login(_account.Username, _account.Password);
            _isInitialized = true;
            //todo save cookies (now or before quitting iteration?)

            return _isInitialized;
        }

        protected override IList<InstaUser> FetchUsersToFill()
        {
            var usersToVisit = _repo.Query<InstaUser>().Where(u => u.Status == UserStatus.New && u.HasRussianText == true && (u.Rank == default || u.Rank > -1)).Take(_batchSize).ToList();

            return usersToVisit;
        }

        protected override bool VisitUserProfileExtended(ProfilePage profilePage, InstaUser user, out InstaUser modified)
        {
            modified = user;
            try
            {
                IEnumerable<Post>? postInfos = null;

                if (profilePage.CheckHasStory())
                {
                    modified.LastPostDate = DateTime.UtcNow;
                }
                else
                {
                    postInfos ??= profilePage.GetLastPosts().ToList();
                    modified.LastPostDate = postInfos.Select(p => p.PublishDate).Max().ToUniversalTime();
                }
                
                if (! modified.HasRussianText == true)
                {
                    postInfos ??= profilePage.GetLastPosts().ToList();
                    user.HasRussianText = postInfos.Any(p => p.Description.HasRussianText());
                }

                modified.Rank = Convert.ToInt32(CalculateRank(modified));
                modified.Status = UserStatus.Visited;

                return true;
            }
            catch(Exception ex) 
            {
                _logger.LogError("Error while getting extended user data. Ex: {0}", ex.Data.Values);
                return false;
            }
        }

        protected override double CalculateRank(InstaUser user)
        {
            var baseRank = base.CalculateRank(user);

            var lastPostDate = user.LastPostDate;
            var lastPostMultiplier = 1;
            if (DateTime.UtcNow - lastPostDate <= TimeSpan.FromDays(30))
                lastPostMultiplier = 2;
            if (DateTime.UtcNow - lastPostDate <= TimeSpan.FromDays(14))
                lastPostMultiplier = 4;
            if (DateTime.UtcNow - lastPostDate <= TimeSpan.FromDays(7))
                lastPostMultiplier = 5;

            return (baseRank * lastPostMultiplier);
        }
    }
}
