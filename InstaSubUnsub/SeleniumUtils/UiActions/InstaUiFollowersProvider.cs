using InstaCommon.Extensions;
using InstaDomain;
using InstaDomain.Account;
using InstaInfrastructureAbstractions;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SeleniumPageObjects;
using SeleniumUtils.Helpers;
using SeleniumUtils.PageObjects;
using SeleniumUtils.UiActions.Base;

namespace SeleniumUtils.UiActions
{
    public class InstaUiFollowersProvider : PersistentAuthActionBase, IFollowersProvider
    {
        public InstaUiFollowersProvider(IWebDriverFactory driverFactory, ILogger<InstaUiUserFollower> logger,
            IConfiguration conf, IPersistentCookieUtil cookieUtil) : base(driverFactory, logger, conf, cookieUtil) { }

        public IList<InstaUser> GetByUser(InstaUser user, InstaAccount account)
        {
            Login(account);

            var followersPage = new FollowersPage(WebDriver, user.Name);
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
