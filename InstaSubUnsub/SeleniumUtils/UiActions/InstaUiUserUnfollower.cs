using InstaDomain;
using InstaDomain.Account;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SeleniumPageObjects;
using SeleniumUtils.Helpers;
using SeleniumUtils.PageObjects;
using SeleniumUtils.UiActions.Base;

namespace SeleniumUtils.UiActions
{
    public class InstaUiUserUnfollower : PersistentAuthActionBase, IUserUnfollower
    {
        public InstaUiUserUnfollower(IWebDriverFactory driverFactory, ILogger<InstaUiUserFollower> logger,
            IConfiguration conf, PersistentCookieUtil cookieUtil) : base(driverFactory, logger, conf, cookieUtil) { }

        public bool Unfollow(InstaUser user, InstaAccount account)
        {
            Login(account);

            var profilePage = new ProfilePage(WebDriver, user.Name);
            profilePage.Load();
            
            return profilePage.Unfollow();
        }
    }
}
