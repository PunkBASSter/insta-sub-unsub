using InstaCommon.Config.Jobs;
using InstaCrawlerApp.Account.Interfaces;
using InstaDomain;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace InstaCrawlerApp.Jobs
{
    public class InstaDbSyncher : JobBase
    {
        private readonly IFollowingsProvider _followingsProvider;

        public InstaDbSyncher(IRepository repo, ILogger<JobBase> logger, InstaDbSyncherJobConfig config, IAccountProvider<InstaDbSyncher> accountProvider, 
            IFollowingsProvider followingsProvider) 
            : base(repo, logger, config, accountProvider)
        {
            _followingsProvider = followingsProvider;
        }

        private InstaUser? _mainAccountUser;
        private InstaUser MainAccountUser
        {
            get
            {
                _mainAccountUser ??= Repository.Query<InstaUser>().First(u => u.Name == Account.Username);
                return _mainAccountUser;
            }
        }

        protected override void ExecuteInternal()
        {
            var dbFollowings = Repository.Query<InstaUser>()
                .Where(u => u.Status == InstaDomain.Enums.UserStatus.Followed)
                .ToList();

            var uiFollowings = _followingsProvider.GetByUser(MainAccountUser, Account)
                .ToList();

            var dbUiMismatch = dbFollowings.Except(uiFollowings, new InstaUserByNameAndStatusEqualityComparer()).ToList();

            foreach(var usr in dbUiMismatch)
            {
                var upd = usr;
                upd.Status = InstaDomain.Enums.UserStatus.Unfollowed;
                upd.UnfollowingDate = DateTime.UtcNow;
                Repository.Update(upd);
                Repository.SaveChanges();
            }
        }

        internal class InstaUserByNameAndStatusEqualityComparer : IEqualityComparer<InstaUser>
        {
            public bool Equals(InstaUser? x, InstaUser? y)
            {
                return x?.Name == y?.Name && x?.Status == y?.Status;
            }

            public int GetHashCode([DisallowNull] InstaUser obj)
            {
                return obj.Name.GetHashCode();
            }
        }
    }
}
