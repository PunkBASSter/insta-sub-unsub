using InstaDomain;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IFollowingsProvider
    {
        IList<InstaUser> GetByUser(InstaUser user, InstaAccount autenticateAs);
    }
}
