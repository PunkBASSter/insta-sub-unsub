using InstaCommon.Extensions;
using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils.PageObjects;

namespace SeleniumUtils.UiActions
{
    public class InstaUiFollowersProvider : UiActionBase, IFollowersProvider
    {
        public InstaUiFollowersProvider(IWebDriver driver, ILogger<InstaUiFollowersProvider> logger, IConfiguration config) : base(driver, logger, config) { }

        protected override string ConfigSectionName => "CrawlUser";

        public IList<InstaUser> GetByUser(InstaUser user, InstaAccount? account = null)
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
