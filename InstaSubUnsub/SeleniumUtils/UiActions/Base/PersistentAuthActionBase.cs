using InstaDomain.Account;
using InstaInfrastructureAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SeleniumPageObjects;
using SeleniumUtils.PageObjects;

namespace SeleniumUtils.UiActions.Base
{
    public abstract class PersistentAuthActionBase : UiActionBase
    {
        private readonly IPersistentCookieUtil _cookieUtil;

        public PersistentAuthActionBase(IWebDriverFactory driverFactory, ILogger<UiActionBase> logger,
            IConfiguration configuration, IPersistentCookieUtil persistentCookieUtil) : base(driverFactory, logger, configuration)
        {
            _cookieUtil = persistentCookieUtil;
        }

        /// <summary>
        /// Tries to load and apply previously saved cookies if possible.
        /// Otherwise does regular UI login and saves cookies for the future.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public override bool Login(InstaAccount account)
        {
            if (LoggedInUsername == account.Username)
                return true;

            var profilePage = new ProfilePage(WebDriver, account.Username);
            profilePage.Load();

            if (!_cookieUtil.LoadCookies(account.Username))
            {
                return UiLogInSaveCookies(account);
            }

            LoggedInUsername ??= account.Username;
            _cookieUtil.SaveCookies(account.Username);

            return true;
        }

        private bool UiLogInSaveCookies(InstaAccount account)
        {
            var res = base.Login(account);
            _cookieUtil.SaveCookies(account.Username);
            return res;
        }
    }
}
