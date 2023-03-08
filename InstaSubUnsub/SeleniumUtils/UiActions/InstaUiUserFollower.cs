using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils.PageObjects;

namespace SeleniumUtils.UiActions
{
    public class InstaUiUserFollower : UiActionBase, IUserFollower
    {
        public InstaUiUserFollower(IWebDriver driver, ILogger<InstaUiUserFollower> logger) : base(driver, logger)
        {
        }

        public bool Follow(InstaUser user, InstaAccount account)
        {
            if (account != null)
                Login(account);

            var profilePage = new ProfilePage(_webDriver, user.Name);
            profilePage.Load();
            return profilePage.Follow();

        }
    }
}
