namespace InstaDomain.Account
{
    public class AccountUsageHistory : InstaAccount
    {
        public AccountUsageHistory(InstaAccount account) : base(account.Username, account.Password) { }

        public DateTime? LastUsedTime { get; init; }

        public string? LastUsedInService { get; init; }

        public int? LastEntitiesProcessed { get; init; }
    }
}
