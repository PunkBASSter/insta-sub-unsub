using InstaCrawlerApp.Jobs;
using InstaDomain;

namespace InstaCrawlerApp.Scheduling
{
    public class JobExecutionDetails : BaseEntity
    {
        public string JobName { get; set; } = nameof(JobBase);
        public DateTime ScheduledAt { get; init; } = DateTime.UtcNow;
        public int LimitPerIteration { get; set; }
        public int ProcessedNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? CompletionTime { get; set; }
        public string ErrorInfo { get; set; } = string.Empty;
    }
}
