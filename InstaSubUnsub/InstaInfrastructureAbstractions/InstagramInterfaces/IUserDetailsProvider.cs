using InstaDomain;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface IUserDetailsProvider
    {
        InstaUser GetUserDetails(InstaUser user, InstaAccount? account=null);
    }
}
