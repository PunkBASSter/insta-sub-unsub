using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils.Helpers;
using SeleniumUtils.PageObjects;

namespace SeleniumUtils.UiActions
{
    public class InstaUiUserUnfollower : PersistentAuthActionBase, IUserUnfollower
    {
        public InstaUiUserUnfollower(IWebDriver driver, PersistentCookieUtil cookieUtil,
            ILogger<InstaUiUserUnfollower> logger, IConfiguration conf) : base(driver, cookieUtil, logger, conf)
        {
        }

        protected override string ConfigSectionName => "FollowUser";

        public bool Unfollow(InstaUser user, InstaAccount? account = null)
        {
            Login(account);

            var profilePage = new ProfilePage(_webDriver, user.Name);
            profilePage.Load();
            
            return profilePage.Unfollow();
        }
    }
}
