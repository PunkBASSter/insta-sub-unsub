using InstaDomain;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IFollowersProvider : ILoggedInUserState
    {
        IList<InstaUser> GetByUser(InstaUser user, InstaAccount autenticateAs);
    }
}
