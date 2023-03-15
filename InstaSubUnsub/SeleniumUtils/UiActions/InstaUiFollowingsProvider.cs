using InstaCommon.Exceptions;
using InstaCommon.Extensions;
using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils.PageObjects;

namespace SeleniumUtils.UiActions
{
    public class InstaUiFollowingsProvider : UiActionBase, IFollowingsProvider
    {
        //Not sure if the UI-based class will be reqired for unsubscribing
        public InstaUiFollowingsProvider(IWebDriver driver, ILogger<UiActionBase> logger, IConfiguration conf) : base(driver, logger, conf)
        {
        }

        protected override string ConfigSectionName => "CrawlUser";

        /// <summary>
        /// Gets the list of users followed by us from InstaUI, the output needs to be filtered by Status != Protected (3)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="LoginFailedException"></exception>
        public IList<InstaUser> GetByUser(InstaUser user, InstaAccount? account=null)
        {
            if (!Login(account))
                throw new LoginFailedException($"Login failed for username {account.Username}");

            var followingsPage = new FollowingPage(_webDriver, user.Name);
            followingsPage.Load();

            var items = followingsPage.InfiniteScrollToBottomWithItemsLoading();
            var result = items.Select(i => new InstaUser
            {
                Name = i.UserName,
                Status = InstaDomain.Enums.UserStatus.Followed,
                HasRussianText = i.Description.HasRussianText(),
                LastPostDate = i.CheckHasStory() ? DateTime.UtcNow : null,
            }).ToList();

            return result;
        }
    }
}
