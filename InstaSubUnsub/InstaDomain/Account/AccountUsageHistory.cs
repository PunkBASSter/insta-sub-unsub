namespace InstaDomain.Account
{
    public class AccountUsageHistory : InstaAccount
    {
        public DateTime? LastUsedTime { get; init; } = DateTime.MinValue;

        public string? LastUsedInService { get; init; } = string.Empty;

        public int? LastEntitiesProcessed { get; init; }

        public DateTime? AntiBotDetectedTime { get; init; }
    }
}
