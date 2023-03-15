using InstaDomain;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface ILogin
    {
        bool Login(InstaAccount? account = null);
    }
}
