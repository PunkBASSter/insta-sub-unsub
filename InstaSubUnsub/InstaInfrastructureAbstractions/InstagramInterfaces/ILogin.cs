using InstaDomain.Account;

namespace InstaInfrastructureAbstractions.InstagramInterfaces
{
    public interface ILogin
    {
        bool Login(InstaAccount account);
    }
}
