using InstaDomain;
using InstaDomain.Account;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IUserDetailsProvider : ILoggedInUserState
    {
        InstaUser GetUserDetails(InstaUser user, InstaAccount account);
    }
}
