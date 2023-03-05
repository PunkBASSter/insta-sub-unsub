using InstaDomain;

namespace InstaInfrastructureAbstractions.DataProviderInterfaces
{
    public interface IFollowingsProvider
    {
        IList<InstaUser> GetByUser(InstaUser user, InstaAccount autenticateAs);
    }
}
