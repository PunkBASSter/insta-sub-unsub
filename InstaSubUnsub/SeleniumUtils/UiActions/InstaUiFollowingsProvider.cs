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
        public InstaUiFollowingsProvider(IWebDriver driver, ILogger<UiActionBase> logger, IConfiguration conf) : base(driver, logger, conf)
        {
        }

        public int Limit { get; set; }

        protected override string ConfigSectionName => "FollowUser";

        /// <summary>
        /// Gets the list of users followed by us from InstaUI, the output needs to be filtered by Status != Protected (3)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="LoginFailedException"></exception>
        public IList<InstaUser> GetByUser(InstaUser user, InstaAccount? account=null)
        {
            Login(account);

            var followingsPage = new FollowingPage(_webDriver, user.Name);
            followingsPage.Load();

            var items = followingsPage.InfiniteScrollToBottomWithItemsLoading(Limit);
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
