using InstaDomain;
using InstaDomain.Account;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IUserFollower : ILoggedInUserState
    {
        bool Follow(InstaUser user, InstaAccount account);
    }
}
