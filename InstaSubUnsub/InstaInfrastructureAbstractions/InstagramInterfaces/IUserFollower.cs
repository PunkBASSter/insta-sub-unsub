using InstaDomain;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IUserFollower
    {
        bool Follow(InstaUser user, InstaAccount account);
    }
}
