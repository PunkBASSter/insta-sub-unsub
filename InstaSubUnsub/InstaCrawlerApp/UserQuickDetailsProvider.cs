using InstaCommon.Exceptions;
using InstaDomain;
using InstaDomain.Enums;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;

namespace InstaCrawlerApp
{
    public class UserQuickDetailsProvider
    {
        protected readonly IUserDetailsProvider _userDetailsProvider;
        protected readonly IRepository _repo;
        protected readonly int _batchSize;
        private int _consequentAntiBotFailures = 0;
        protected readonly ILogger<UserQuickDetailsProvider> _logger;

        public UserQuickDetailsProvider(IUserDetailsProvider userDetailsProvider, IRepository repo, ILogger<UserQuickDetailsProvider> logger)
        {
            _userDetailsProvider = userDetailsProvider;
            _repo = repo;
            _batchSize = 200 + new Random(DateTime.Now.Microsecond).Next(-14, 17);
            _logger = logger;
        }

        public void ProvideDetails()
        {
            var users = FetchUsersToFill();

            if (users.Count < 1 || !Initialize())
                return;

            _logger.LogInformation("Started {0} iteration. Max BatchSize is {1}", GetType().Name, _batchSize);

            var processed = 0;
            var rankAbove3 = 0;
            var errors = 0;
            foreach (var user in users)
            {
                if (_consequentAntiBotFailures >= 3)
                {
                    _logger.LogWarning("Consequent anti-bot errors number equals 3. Stopping...", user.Name);
                    break;
                }

                if (VisitUserProfile(user, out InstaUser modified))
                {
                    _repo.Update(modified);
                    _repo.SaveChanges();
                    if (modified.Rank > 3) rankAbove3++;
                    processed++;
                }
                else if (modified.Status == UserStatus.Error || modified.Status == UserStatus.Unavailable)
                {
                    _repo.Update(modified);
                    _repo.SaveChanges();
                    errors++;
                }
            }

            _logger.LogInformation("Finished {0} iteration. Successfully processed {1} of {2} fetched. Errors: {3}. RankAboveZero items: {4}",
                GetType().Name, processed, users.Count, errors, rankAbove3);
        }

        protected virtual bool Initialize() { return true; }

        protected virtual IList<InstaUser> FetchUsersToFill()
        {
            throw new NotImplementedException("Unathenticated qiuck data mining is not supposed to be supported.");

            return _repo.Query<InstaUser>().Where(u => u.Status == UserStatus.New
                && u.HasRussianText == true
                && u.IsClosed != true
                && u.Rank == 0
                && u.FollowingDate == null
                && u.UnfollowingDate == null
             )
            .Take(_batchSize).ToList();
        }

        protected bool VisitUserProfile(InstaUser user, out InstaUser modified)
        {
            modified = user;
            try
            {
                modified = _userDetailsProvider.GetUserDetails(user);
                return modified != null;
            }
            catch (InstaAntiBotException ex)
            {
                _logger.LogError(ex, ex.Message, ex.Data.Values);
                _consequentAntiBotFailures++;
                return false;
            }
            catch (UserPageUnavailable ex)
            {
                _logger.LogWarning(ex, ex.Message, ex.Data.Values);
                modified.Status = UserStatus.Unavailable;
                return false;
            }
            catch (Exception ex)
            {
                modified.Status = UserStatus.Error;
                _logger.LogError("Unexpected error:", ex.Data.Values);
                _logger.LogError(ex, ex.Message, ex.Data.Values);
                return false;
            }
        }
    }
}
