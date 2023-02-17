using InstaDomain;
using UserStatus = InstaDomain.Enums.UserStatus;

namespace InstaPersistence.DataSeed
{
    public class ProtectedInstaUsers
    {
        public IEnumerable<InstaUser> GetSeedData()
        {
            var users = new List<InstaUser>
            {
                new InstaUser { Id = 1, Name = "hidethetrack123", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 2, Name = "doctor_lilith", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 3, Name = "dr.imiller", IsFollower = true, Status = UserStatus.Protected },

                new InstaUser { Id = 4, Name = "mynameiswhm", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 5, Name = "err_yep", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 6, Name = "temapunk", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 7, Name = "sergesoukonnov", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 8, Name = "panther_amanita", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 9, Name = "igor.gord", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 10, Name = "olga.mikholenko", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 11, Name = "iriska_sia", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 12, Name = "oli4kakisskiss", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 13, Name = "err_please", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 14, Name = "blefamer", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 15, Name = "lodkaissad", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 16, Name = "anastasiya_kun", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 17, Name = "anna.saulenko", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 18, Name = "prikhodko5139", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 19, Name = "saulenkosvetlana", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 20, Name = "ira_knows_best", IsFollower = true, Status = UserStatus.Protected },

                new InstaUser { Id = 21, Name = "meltali_handmade", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 22, Name = "dr.ksusha_pro_edu", IsFollower = true, Status = UserStatus.Protected },
                new InstaUser { Id = 23, Name = "lydok87", IsFollower = true, Status = UserStatus.Protected },


                //TODO Add the rest of subscriptions for testing purposes for my account,
                //Get protected subs for Ira's account
            };

            return users;
        }
    }
}
