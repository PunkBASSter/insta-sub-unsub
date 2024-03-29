﻿using InstaCommon.Exceptions;
using InstaCommon.Extensions;
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
    public class InstaUiFollowingsProvider : PersistentAuthActionBase, IFollowingsProvider
    {
        public InstaUiFollowingsProvider(IWebDriverFactory driverFactory, ILogger<InstaUiUserFollower> logger,
            IConfiguration conf, IPersistentCookieUtil cookieUtil) : base(driverFactory, logger, conf, cookieUtil) { }

        public int Limit { get; set; }

        /// <summary>
        /// Gets the list of users followed by us from InstaUI, the output needs to be filtered by Status != Protected (3)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="LoginFailedException"></exception>
        public IList<InstaUser> GetByUser(InstaUser user, InstaAccount account)
        {
            Login(account);

            var followingsPage = new FollowingPage(WebDriver, user.Name);
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
