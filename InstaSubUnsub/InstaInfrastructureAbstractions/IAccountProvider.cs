using InstaDomain.Account;

namespace InstaInfrastructureAbstractions
{
    public interface IAccountProvider<out TConsumer> : IAccountUsageHistorySaver where TConsumer : class
    {
        InstaAccount Get();
    }
}
