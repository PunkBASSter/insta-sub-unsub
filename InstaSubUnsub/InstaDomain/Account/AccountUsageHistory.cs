using Domain.Account;

namespace InstaDomain.Account
{
    public class AccountUsageHistory : InstaAccount
    {
        //Ctor added to allow creating migrations
        public AccountUsageHistory() : base("TestUser", "TestPassword") { }
        public AccountUsageHistory(InstaAccount account) : base(account.Username, account.Password) { }

        public DateTime? LastUsedTime { get; init; }

        public string? LastUsedInService { get; init; }

        public int? LastEntitiesProcessed { get; init; }
    }
}
