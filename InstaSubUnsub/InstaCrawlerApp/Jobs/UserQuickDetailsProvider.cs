using InstaCommon.Config.Jobs;
using InstaCommon.Exceptions;
using InstaCrawlerApp.Account.Interfaces;
using InstaDomain;
using InstaDomain.Enums;
using InstaInfrastructureAbstractions.InstagramInterfaces;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using Microsoft.Extensions.Logging;

namespace InstaCrawlerApp.Jobs
{
    public abstract class UserQuickDetailsProvider : JobBase
    {
        protected readonly IUserDetailsProvider _userDetailsProvider;
        protected readonly IRepository _repo;
        private int _consequentAntiBotFailures = 0;
        protected readonly ILogger<UserQuickDetailsProvider> _logger;

        public UserQuickDetailsProvider(IUserDetailsProvider userDetailsProvider, IRepository repo,
            ILogger<UserQuickDetailsProvider> logger, UserFullDetailsProviderJobConfig config,
            IAccountProvider<UserQuickDetailsProvider> accProvider) : base(repo, logger, config, accProvider)
        {
            _userDetailsProvider = userDetailsProvider;
            _repo = repo;
            _logger = logger;
        }

        protected override int ExecuteInternal()
        {
            var users = FetchUsersToFill();

            if (users.Count < 1 || !Initialize())
                return 0;

            _logger.LogInformation("Started {0} iteration. Max BatchSize is {1}", GetType().Name, LimitPerIteration);

            var processed = 0;
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
                    processed++;
                }
                else if (modified.Status == UserStatus.Error || modified.Status == UserStatus.Unavailable)
                {
                    _repo.Update(modified);
                    _repo.SaveChanges(); ;
                }
            }

            return processed;
        }

        protected virtual bool Initialize() { return true; }

        protected abstract IList<InstaUser> FetchUsersToFill();

        protected bool VisitUserProfile(InstaUser user, out InstaUser modified)
        {
            modified = user;
            try
            {
                modified = _userDetailsProvider.GetUserDetails(user, Account);
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
