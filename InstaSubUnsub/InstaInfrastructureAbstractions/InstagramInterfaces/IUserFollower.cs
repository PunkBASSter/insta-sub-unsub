using InstaDomain;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IUserFollower : ILoggedInUserState
    {
        bool Follow(InstaUser user, InstaAccount? account = null);
    }
}
