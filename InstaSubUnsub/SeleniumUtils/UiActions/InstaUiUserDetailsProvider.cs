using InstaCommon;
using InstaCommon.Extensions;
using InstaDomain;
using InstaDomain.Enums;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils.Extensions;
using SeleniumUtils.Helpers;
using SeleniumUtils.PageObjects;
using SeleniumUtils.UiActions.Base;

namespace SeleniumUtils.UiActions
{
    public class InstaUiUserDetailsProvider : PersistentAuthActionBase, IUserDetailsProvider
    {
        private const double MinimumRank = 3.0; //minimum ratio of followings/followers to proceed with user data mining

        public InstaUiUserDetailsProvider(IWebDriver driver, ILogger<InstaUiUserFollower> logger,
            IConfiguration conf, PersistentCookieUtil cookieUtil) : base(driver, logger, conf, cookieUtil) { }

        protected override string ConfigSectionName => "CrawlUser";

        protected virtual InstaUser VisitUserProfileExtended(ProfilePage profilePage, InstaUser user)
        {
            //Data available only for logged in users
            var modified = user;
            IEnumerable<Post>? postInfos = null;

            if (profilePage.CheckHasStory())
            {
                modified.LastPostDate = DateTime.UtcNow;
            }
            else
            {
                postInfos ??= profilePage.GetLastPosts().ToList();
                if (postInfos.Any())
                    modified.LastPostDate = postInfos.Select(p => p.PublishDate).Max().ToUniversalTime();
            }

            if (!modified.HasRussianText == true)
            {
                postInfos ??= profilePage.GetLastPosts().ToList();
                user.HasRussianText = postInfos.Any(p => p.Description.HasRussianText());
            }

            return modified;
        }

        public InstaUser GetUserDetails(InstaUser user, InstaAccount? account = null)
        {
            Login(account);

            //Data available for anonymous users
            var detailedUser = user;
            var profilePage = new ProfilePage(_webDriver, user.Name);

            if (!profilePage.Load())
                return detailedUser;

            var followersNum = profilePage.FollowersNumElement.GetInstaSubNumber();
            var followingsNum = profilePage.FollowingsNumElement.GetInstaSubNumber();
            var hasRussianText = profilePage.GetDescriptionText().HasRussianText();
            detailedUser.FollowersNum = followersNum;
            detailedUser.FollowingsNum = followingsNum;
            detailedUser.HasRussianText = hasRussianText;
            detailedUser.Rank = CalculateRank(detailedUser);
            detailedUser.Status = UserStatus.Visited;
            detailedUser.IsClosed = profilePage.CheckClosedAccount();

            if (detailedUser.IsClosed != true && !string.IsNullOrWhiteSpace(LoggedInUsername) && detailedUser.Rank >= MinimumRank)
                //Data available for logged in users
                detailedUser = VisitUserProfileExtended(profilePage, detailedUser);

            new Delay(1831, 3342).Random();

            return detailedUser;
        }

        protected virtual double CalculateRank(InstaUser user)
        {
            var followers = Math.Max(user.FollowersNum ?? 1, 1);
            var followings = user.FollowingsNum ?? 0;

            var followingsRatio = followings / followers;

            return followingsRatio;
        }
    }
}
