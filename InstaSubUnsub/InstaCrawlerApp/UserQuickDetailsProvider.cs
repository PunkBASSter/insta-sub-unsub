using InstaDomain;
using InstaDomain.Enums;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using SeleniumUtils;
using SeleniumUtils.Extensions;
using SeleniumUtils.PageObjects;

namespace InstaCrawlerApp
{   
    /// <summary>
    /// Updates user DB record with the number of followers and followings and presence of Russian
    /// characters in the profile description.
    /// Takes users with Status==New, updates Rank, FollowersNum, FollowingsNum and HasRussianText.
    /// </summary>
    public class UserQuickDetailsProvider
    {
        private readonly ProfilePage _profilePage;
        private readonly IRepository _repo;
        private readonly int _batchSize;
        private int _consequentAntiBotFailures = 0;

        public UserQuickDetailsProvider(ProfilePage profilePage, IRepository repo)
        {
            _profilePage= profilePage;
            _repo= repo;
            _batchSize = 200 + new Random(DateTime.Now.Microsecond).Next(-14, 17); ;
        }

        public void AnonymousProvideDetails()
        {
            var usrQuery = _repo.TrackedQuery<InstaUser>();
            var users = usrQuery
                .Where(u => u.Status == UserStatus.New && u.Rank == default).Take(_batchSize).ToList();
            foreach (var user in users)
            {
                if (_consequentAntiBotFailures >= 3)
                    return; //Maybe indicate about extra timeout.

                if (VisitUserProfile(user, out InstaUser modified))
                {
                    _repo.Update(modified);
                    _repo.SaveChanges();
                }       
            }
        }

        private bool VisitUserProfile(InstaUser user, out InstaUser modified)
        {
            modified = user;
            try
            {
                if (!_profilePage.Load(user.Name))
                    return false;
                    
                _consequentAntiBotFailures = 0;
                //Modify Load to detect possible insta's bans/blocks
                //TODO consequent page loading errors handling (Instagram bot resistance)

                var followersNum = _profilePage.FollowersNumElement.GetInstaSubNumber();
                var followingsNum = _profilePage.FollowingsNumElement.GetInstaSubNumber();
                var hasRussianText = _profilePage.GetDescriptionText().HasRussianText();
                modified.FollowersNum = followersNum;
                modified.FollowingsNum = followingsNum;
                modified.HasRussianText = hasRussianText;
                modified.Rank = Convert.ToInt32(CalculateRank(followersNum, followingsNum, hasRussianText));
                new Delay(1831, 3342).Random();
                return true;
            }
            catch (InstaAntiBotException ex)
            {
                //todo add logging
                _consequentAntiBotFailures++;
                return false;
            }
            catch (Exception ex)
            {
                //todo add logging
                return false;
            }
        }

        //Consider accepting user obj
        private double CalculateRank(double followers, double followings, bool hasRussianText)
        {
            followers = Math.Max(followers, 1);

            var followingsRatio = followings / followers;
            if (followingsRatio < 3)
                return -1;

            var rusMultimplier = 1 + Convert.ToByte(hasRussianText) * 3;

            return (followingsRatio * rusMultimplier);
        }
    }
}
