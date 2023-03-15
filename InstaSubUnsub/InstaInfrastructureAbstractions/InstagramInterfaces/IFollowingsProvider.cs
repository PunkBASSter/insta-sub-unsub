using InstaDomain;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IFollowingsProvider : ILoggedInUserState, ILogin
    {
        IList<InstaUser> GetByUser(InstaUser user, InstaAccount? autenticateAs=null);
    }
}
