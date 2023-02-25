using InstaDomain;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using SeleniumUtils;
using SeleniumUtils.PageObjects;
using InstaDomain.Enums;
using SeleniumUtils.Extensions;

namespace InstaCrawlerApp
{
    public class UserDetailsProvider
    {
        private readonly LoginPage _loginPage;
        private readonly ProfilePage _profilePage;
        private readonly Account _account;
        private bool _isInitialized = false;
        private readonly IRepository _repo;
        private readonly int _visitsLimitPerIteration = 287;

        public UserDetailsProvider(LoginPage loginPage, ProfilePage profilePage, IRepository repo, Account account)
        {
            _loginPage = loginPage;
            _profilePage = profilePage;
            _repo = repo;
            _account = account;
            _visitsLimitPerIteration += new Random(DateTime.Now.Microsecond).Next(-83, 84); //randomizing the iteration limit
        }

        public void Initialize()
        {
            if (_isInitialized) return;

            _loginPage.Load();
            _loginPage.Login(_account.Username, _account.Password);
            _loginPage.HandleAfrerLoginQuestions();
            _isInitialized = true;
            //todo save cookies (now or before quitting iteration?)
        }

        public void ProvideUserDetails()
        {
            Initialize();

            var visited = 0;
            var usersToVisit = _repo.Query<InstaUser>().Where(u => u.Status == UserStatus.New && u.HasRussianText == true).ToList();

            if (!usersToVisit.Any())
                usersToVisit = _repo.Query<InstaUser>().Where(u => u.Status == UserStatus.New && u.HasRussianText == null).ToList();

            foreach (var user in usersToVisit)
            {
                if (visited >= _visitsLimitPerIteration)
                    break;

                var userDetails = VisitUserProfile(user);
                _repo.Update(userDetails);
                _repo.SaveChanges();

                new Delay().Random();
                visited++;
            }
        }

        private InstaUser VisitUserProfile(InstaUser user)
        {
            try
            {
                _profilePage.Load(user.Name);
                //TODO handle obsolete user names when page is not loaded properly

                
                var followersNum = _profilePage.FollowersNumElement.GetInstaSubNumber();
                var followingsNum = _profilePage.FollowingsNumElement.GetInstaSubNumber();
                user.FollowersNum = followersNum;
                user.FollowingsNum = followingsNum;
                user.Status = UserStatus.Visited;

                var ratio = followingsNum / Math.Max(followersNum, 1);
                if (ratio < 3) //consider as useless subs and skip
                {
                    user.Rank = -1;
                    return user;
                }

                try
                {
                    var postInfos = _profilePage.GetLastPosts().ToList();
                    var lastPostDate = postInfos.Select(p => p.PublishDate).Max().ToUniversalTime();
                    var hasRussianText = postInfos.Any(p => p.Description.HasRussianText()); //TODO Deal with empty descriptions
                    var rank = Convert.ToInt32(CalculateRank(followersNum, followingsNum, lastPostDate, hasRussianText));


                    user.LastPostDate = lastPostDate;
                    user.HasRussianText = user.HasRussianText == true || hasRussianText;
                    user.Rank = rank;
                }
                catch(Exception ex) 
                { 
                    user.Status = UserStatus.Visited;
                    user.Rank = Convert.ToInt32(CalculateRank(followersNum, followingsNum, default, user.HasRussianText == true));
                }

                
            }
            catch (Exception ex)
            {
                //todo logging
                user.Status = UserStatus.Error;
            }

            return user;
        }

        private double CalculateRank(double followers, double followings, DateTime lastPostDate, bool hasRussianText)
        {
            followers = Math.Max(followers, 1);

            var followingsRatio = followings / followers;
            followingsRatio = Math.Max(followingsRatio, 0.1);

            //todo make more readable and maintainable
            var lastPostMultiplier = 1;
            if (DateTime.UtcNow - lastPostDate <= TimeSpan.FromDays(30))
                lastPostMultiplier = 2;
            if (DateTime.UtcNow - lastPostDate <= TimeSpan.FromDays(14))
                lastPostMultiplier = 4;
            if (DateTime.UtcNow - lastPostDate <= TimeSpan.FromDays(7))
                lastPostMultiplier = 5;


            var rusMultimplier = 1 + Convert.ToByte(hasRussianText) * 3;

            return (followingsRatio * lastPostMultiplier * rusMultimplier);
        }
    }
}
