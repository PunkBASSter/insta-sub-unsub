using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SeleniumUtils.PageObjects;

namespace SeleniumUtils.UiActions
{
    public class InstaUiUserFollower : UiActionBase, IUserFollower
    {
        private readonly int _postsToLike = 2;

        public InstaUiUserFollower(IWebDriver driver, ILogger<InstaUiUserFollower> logger, IConfiguration conf) : base(driver, logger, conf)
        {
        }

        protected override string ConfigSectionName => "FollowUser";

        public bool Follow(InstaUser user, InstaAccount? account = null)
        {
            Login(account);

            var profilePage = new ProfilePage(_webDriver, user.Name);
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
