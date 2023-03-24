using InstaDomain;
using InstaDomain.Account;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IFollowingsProvider : ILoggedInUserState, ILogin, ILimitedDataProvider
    {
        IList<InstaUser> GetByUser(InstaUser user, InstaAccount autenticateAs);
    }
}
