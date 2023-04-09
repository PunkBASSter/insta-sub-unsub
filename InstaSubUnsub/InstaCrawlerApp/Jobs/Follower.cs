using InstaCommon.Config.Jobs;
using InstaCommon.Exceptions;
using InstaCrawlerApp.Account.Interfaces;
using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;

namespace InstaCrawlerApp.Jobs
{
    public class Follower : JobBase
    {
        private readonly IUserFollower _userFollower;
        private readonly IRepository _repo;

        public Follower(IUserFollower userFollower, IRepository repo, ILogger<Follower> logger,
            FollowerJobConfig config, IAccountProvider<Follower> accountProvider)
            : base(repo, logger, config, accountProvider)
        {
            _userFollower = userFollower;
            _repo = repo;
        }

        protected override void ExecuteInternal()
        {
            var usersToFollow = _repo.Query<InstaUser>().Where(new UsersToFollowFilter().Get())
                .OrderByDescending(u => u.LastPostDate)
                .Take(LimitPerIteration)
                .ToArray();

            foreach (var user in usersToFollow)
            {
                try
                {
                    if (_userFollower.Follow(user, Account))
                    {
                        var updated = user;
                        updated.FollowingDate = DateTime.UtcNow;
                        updated.Status = InstaDomain.Enums.UserStatus.Followed; //todo condider deprecating status
                        _repo.Update(updated);
                        _repo.SaveChanges();
                        ItemsProcessedPerIteration++;
                    }
                }
                catch(UserPageUnavailable ex) 
                {
                    var updated = user;
                    updated.Status = InstaDomain.Enums.UserStatus.Error; 
                    _repo.Update(updated);
                    _repo.SaveChanges();
                }
                catch(InstaAntiBotException ex)
                { 
                    throw ex; 
                }
                //Consider catching everything here or delegate it to the caller
            }
        }

        public class UsersToFollowFilter
        {
            public Func<InstaUser, bool> Get()
            {
                return u => u.Rank >= 3 && u.HasRussianText == true && u.IsClosed != true
                    && u.LastPostDate >= DateTime.UtcNow.Date.AddDays(-7)
                    && u.FollowingDate == null && u.UnfollowingDate == null;
            }
        }
    }
}
