
namespace InstaDomain.Account
{
    public class AccountUsageHistory : InstaAccount
    {
        public DateTime? LastUsedTime { get; init; }

        public string? LastUsedInService { get; init; }

        public int? LastEntitiesProcessed { get; init; }
    }
}
