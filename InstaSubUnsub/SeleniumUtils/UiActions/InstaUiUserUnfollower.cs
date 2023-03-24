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
    public class InstaUiUserUnfollower : PersistentAuthActionBase, IUserUnfollower
    {
        public InstaUiUserUnfollower(IWebDriver driver, ILogger<InstaUiUserFollower> logger,
            IConfiguration conf, PersistentCookieUtil cookieUtil) : base(driver, logger, conf, cookieUtil) { }

        public bool Unfollow(InstaUser user, InstaAccount account)
        {
            Login(account);

            var profilePage = new ProfilePage(_webDriver, user.Name);
            profilePage.Load();
            
            return profilePage.Unfollow();
        }
    }
}
