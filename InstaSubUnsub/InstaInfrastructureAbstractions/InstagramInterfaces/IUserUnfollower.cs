using InstaDomain;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IUserUnfollower : ILogin, ILoggedInUserState
    {
        bool Unfollow(InstaUser user, InstaAccount? account = null);
    }
}
