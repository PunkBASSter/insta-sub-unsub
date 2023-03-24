using InstaCommon.Extensions;
using InstaDomain;
using InstaDomain.Account;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils.Helpers;
using SeleniumUtils.PageObjects;
using SeleniumUtils.UiActions.Base;

namespace SeleniumUtils.UiActions
{
    public class InstaUiFollowersProvider : PersistentAuthActionBase, IFollowersProvider
    {
        public InstaUiFollowersProvider(IWebDriver driver, ILogger<InstaUiUserFollower> logger,
            IConfiguration conf, PersistentCookieUtil cookieUtil) : base(driver, logger, conf, cookieUtil) { }

        public IList<InstaUser> GetByUser(InstaUser user, InstaAccount account)
        {
            Login(account);

            var followersPage = new FollowersPage(_webDriver, user.Name);
            followersPage.Load();

            var items = followersPage.InfiniteScrollToBottomWithItemsLoading();
            var result = items.Select(i => new InstaUser
            {
                Name = i.UserName,
                Status = InstaDomain.Enums.UserStatus.New,
                HasRussianText = i.Description.HasRussianText(),
                LastPostDate = i.CheckHasStory() ? DateTime.UtcNow : null,
            }).ToList();

            return result;
        }
    }
}
