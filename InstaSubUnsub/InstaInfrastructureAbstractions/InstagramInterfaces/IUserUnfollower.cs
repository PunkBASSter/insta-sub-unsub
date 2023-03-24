using InstaDomain;
using InstaDomain.Account;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IUserUnfollower : ILogin, ILoggedInUserState
    {
        bool Unfollow(InstaUser user, InstaAccount account);
    }
}
