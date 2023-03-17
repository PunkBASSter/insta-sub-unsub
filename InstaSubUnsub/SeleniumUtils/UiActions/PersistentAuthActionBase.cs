using InstaDomain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils.Helpers;
using SeleniumUtils.PageObjects;

namespace SeleniumUtils.UiActions
{
    public class PersistentAuthActionBase : UiActionBase
    {
        private readonly PersistentCookieUtil _cookieUtil;

        public PersistentAuthActionBase(IWebDriver driver, PersistentCookieUtil persistentCookieUtil, ILogger<UiActionBase> logger, IConfiguration configuration) : base(driver, logger, configuration)
        {
            _cookieUtil = persistentCookieUtil;
        }

        protected override string ConfigSectionName => "FollowUser";

        /// <summary>
        /// Tries to load and apply previously saved cookies if possible.
        /// Otherwise does regular UI login and saves cookies for the future.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public override bool Login(InstaAccount? account = null)
        {
            account ??= GetInstaAccount();
            var profilePage = new ProfilePage(_webDriver, account.Username);
            profilePage.Load();

            if (!_cookieUtil.LoadCookies())
            {
                return UiLogInSaveCookies(account);
            }

            profilePage.Load();
            if (!profilePage.IsLoggedIn())
            {
                return UiLogInSaveCookies(account);
            }

            LoggedInUsername ??= account.Username;
            _cookieUtil.SaveCookies();

            return true;
        }

        private bool UiLogInSaveCookies(InstaAccount? account) 
        {
            var res = base.Login(account);
            _cookieUtil.SaveCookies();
            return res;
        }
    }
}
