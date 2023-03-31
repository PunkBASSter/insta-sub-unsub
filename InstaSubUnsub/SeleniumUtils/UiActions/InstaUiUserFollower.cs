using InstaDomain;
using InstaDomain.Account;
using InstaInfrastructureAbstractions;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SeleniumPageObjects;
using SeleniumUtils.PageObjects;
using SeleniumUtils.UiActions.Base;

namespace SeleniumUtils.UiActions
{
    public class InstaUiUserFollower : PersistentAuthActionBase, IUserFollower
    {
        private readonly int _postsToLike = 2;

        public InstaUiUserFollower(IWebDriverFactory driverFactory, ILogger<InstaUiUserFollower> logger, 
            IConfiguration conf, IPersistentCookieUtil cookieUtil) : base(driverFactory, logger, conf, cookieUtil)
        {
        }

        public bool Follow(InstaUser user, InstaAccount account)
        {
            Login(account);

            var profilePage = new ProfilePage(WebDriver, user.Name);
            profilePage.Load();

            //Leave likes under last 2 posts
            for (var i = 0; i< _postsToLike; i++)
            {
                var openingAttempts = 2;
                Post? post;
                while (!profilePage.OpenPost(out post, i) && openingAttempts > 0)
                    openingAttempts--;

                if (post != null)
                {
                    post.LikeWithRetries(2);
                    post.Close();
                }
            }
            
            return profilePage.Follow();
        }
    }
}
