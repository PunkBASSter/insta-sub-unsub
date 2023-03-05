using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Configuration;
using InstaDomain.Enums;

namespace InstaCrawlerApp
{
    /// <summary>
    /// Acts on behalf of main account owner
    /// </summary>
    public class Unfollower
    {
        private readonly IRepository _repo;
        private readonly IConfiguration _configuration;
        private readonly int _unfollowLimitPerIteration;

        public Unfollower(IRepository repo, IConfiguration configuration, InstaAccount account)
        {
            _repo = repo;
            _configuration = configuration;
            _unfollowLimitPerIteration = Convert.ToInt32(_configuration.GetSection("Unfollow:LimitPerIteration").Value);
        }

        public void Unfollow()
        {
//            var users = GetFollowingFromUi();
//            SaveUsersToDb(users);

            var usersToUnfollow = _repo.Query<InstaUser>()                
                .Where(usr => usr.Status == UserStatus.Followed)
                .OrderBy(usr => usr.FollowingDate)
                .Take(_unfollowLimitPerIteration)
                .ToList();

            foreach(var user in usersToUnfollow)
            {
                //var uiFollowingItem = _followingPage.FollowingItems.FirstOrDefault(item => item.UserName == user.Name);
                //if (uiFollowingItem is null)
                //{
                //    //TODO logging of unsynchronized data between DB and UI
                //    UnfollowUserInDb(user);
                //    continue;
                //}

                //var success = uiFollowingItem.Unfollow();
                //if (success)
                //{
                //    UnfollowUserInDb(user);
                //}
                //else { } -- //TODO logging of unsynchronized data between DB and UI
            }

        }

        private void SaveUsersToDb(IEnumerable<InstaUser> users)
        {
            foreach (var user in users)
            {
                _repo.InsertOrSkip(user, u => u.Name == user.Name);
            }
            _repo.SaveChanges();
        }

        private void UnfollowUserInDb(InstaUser user)
        {
            user.Status = UserStatus.Unfollowed;
            user.UnfollowingDate = DateTime.UtcNow;
            _repo.Update(user);
            _repo.SaveChanges();
        }
    }
}
