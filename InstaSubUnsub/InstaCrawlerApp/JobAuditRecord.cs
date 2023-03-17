using InstaDomain;

namespace InstaCrawlerApp
{
    public class JobAuditRecord : BaseEntity
    {
        public string JobName { get; set; } = nameof(JobBase);
        public int LimitPerIteration { get; set; }
        public int ProcessedNumber { get; set; }
        public DateTime ExecutionStart { get; set; } = DateTime.UtcNow;
        public DateTime ExecutionEnd { get; set; }
        public string ErrorInfo { get; set; } = string.Empty;
    }
}
