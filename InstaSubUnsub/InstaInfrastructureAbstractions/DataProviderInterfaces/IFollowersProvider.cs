using InstaDomain;

namespace InstaInfrastructureAbstractions.DataProviderInterfaces
{
    public interface IFollowersProvider
    {
        IList<InstaUser> GetByUser(InstaUser user, InstaAccount autenticateAs);
    }
}
