namespace InstaDomain.Account
{
    public class AccountUsageHistory : InstaAccount
    {
        public DateTime LastUsedTime { get; set; } = default;

        public string LastUsedInService { get; set; } = string.Empty;

        public int LastEntitiesProcessed { get; set; }

        public DateTime? AntiBotDetectedTime { get; set; }
    }
}
