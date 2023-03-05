using InstaDomain;

namespace InstaInfrastructureAbstractions.DataProviderInterfaces
{
    public interface IUserDetailsProvider
    {
        InstaUser GetUserDetails(InstaUser user, InstaAccount? account=null);
    }
}
