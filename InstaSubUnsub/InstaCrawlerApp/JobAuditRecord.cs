using InstaDomain;

namespace InstaCrawlerApp
{
    public class JobAuditRecord : BaseEntity
    {
        public string JobName { get; init; } = nameof(JobBase);
        public string AccountName { get; set; } = nameof(InstaAccount);
        public int LimitPerIteration { get; set; }
        public int ProcessedNumber { get; set; }
        public DateTime ExecutionStart { get; init; } = DateTime.UtcNow;
        public DateTime ExecutionEnd { get; set; }
        public string ErrorInfo { get; set; } = string.Empty;
    }
}
