using InstaDomain;
using InstaDomain.Enums;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils;
using SeleniumUtils.Exceptions;
using SeleniumUtils.Extensions;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{
    /// <summary>
    /// Updates user DB record with the number of followers and followings and presence of Russian
    /// characters in the profile description.
    /// Takes users with Status==New, updates Rank, FollowersNum, FollowingsNum and HasRussianText.
    /// </summary>
    public class UserQuickDetailsProvider
    {
        protected readonly IWebDriver _webDriver;
        protected readonly IRepository _repo;
        protected readonly int _batchSize;
        private int _consequentAntiBotFailures = 0;
        protected readonly ILogger<UserQuickDetailsProvider> _logger;

        public UserQuickDetailsProvider(IWebDriver driver, IRepository repo, ILogger<UserQuickDetailsProvider> logger)
        {
            _webDriver = driver;
            _repo= repo;
            _batchSize = 200 + new Random(DateTime.Now.Microsecond).Next(-14, 17);
            _logger = logger;
        }

        public void ProvideDetails()
        {
            var users = FetchUsersToFill();

            if (users.Count < 1 || !Initialize())
                return;

            _logger.LogInformation("Started {0} iteration. Max BatchSize is {1}", GetType().Name, _batchSize);

            var processed = 0;
            var rankAboveZero = 0;
            var errors = 0;
            foreach (var user in users)
            {
                if (_consequentAntiBotFailures >= 3)
                {
                    _logger.LogWarning("Consequent anti-bot errors number equals 3. Stopping...", user.Name);
                    break;
                }

                if (VisitUserProfile(user, out InstaUser modified))
                {
                    _repo.Update(modified);
                    _repo.SaveChanges();
                    if (modified.Rank > 0) rankAboveZero++;
                    processed++;
                }
                else if (modified.Status == UserStatus.Error || modified.Status == UserStatus.Unavailable)
                {
                    _repo.Update(modified);
                    _repo.SaveChanges();
                    errors++;
                }
            }

            _logger.LogInformation("Finished {0} iteration. Successfully processed {1} of {2} fetched. Errors: {3}. RankAboveZero items: {4}",
                GetType().Name, processed, users.Count, errors, rankAboveZero);
        }

        protected virtual bool Initialize() { return true; }

        protected virtual IList<InstaUser> FetchUsersToFill()
        {
            return _repo.Query<InstaUser>().Where(u => u.Status == UserStatus.New && u.Rank == default).Take(_batchSize).ToList();
        }

        protected virtual bool VisitUserProfileExtended(ProfilePage profilePage, InstaUser user, out InstaUser modified)
        {
            modified = user;
            return true; 
        }

        protected bool VisitUserProfile(InstaUser user, out InstaUser modified)
        {
            modified = user;
            var profilePage = new ProfilePage(_webDriver);
            try
            {
                if (!profilePage.Load(user.Name))
                    return false;
                    
                _consequentAntiBotFailures = 0;

                var followersNum = profilePage.FollowersNumElement.GetInstaSubNumber();
                var followingsNum = profilePage.FollowingsNumElement.GetInstaSubNumber();
                var hasRussianText = profilePage.GetDescriptionText().HasRussianText();
                modified.FollowersNum = followersNum;
                modified.FollowingsNum = followingsNum;
                modified.HasRussianText = hasRussianText;
                modified.Rank = Convert.ToInt32(CalculateRank(modified));

                var result = true;
                if (modified.Rank > 0)
                    result = VisitUserProfileExtended(profilePage, modified, out modified);
                                
                new Delay(1831, 3342).Random();

                return result;
            }
            catch (InstaAntiBotException ex)
            {
                _logger.LogError(ex, ex.Message, ex.Data.Values); //TODO make sure it's optimal set of params
                _consequentAntiBotFailures++;
                return false;
            }
            catch (UserPageUnavailable ex)
            {
                _logger.LogWarning(ex, ex.Message, ex.Data.Values);
                modified.Status = UserStatus.Unavailable;
                return false;
            }
            catch (Exception ex)
            {
                modified.Status = UserStatus.Error;
                _logger.LogError("Unexpected error:", ex.Data.Values);
                _logger.LogError(ex, ex.Message, ex.Data.Values);
                return false;
            }
        }

        protected virtual double CalculateRank(InstaUser user)
        {
            var followers = Math.Max(user.FollowersNum ?? 1, 1);
            var followings = user.FollowingsNum ?? 0;

            var followingsRatio = followings / followers;
            if (followingsRatio < 3)
                return -1;

            var rusMultimplier = 1 + Convert.ToByte(user.HasRussianText ?? false) * 3;

            return (followingsRatio * rusMultimplier);
        }
    }
}
