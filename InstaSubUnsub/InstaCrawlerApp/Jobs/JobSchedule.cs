using InstaDomain;

namespace InstaCrawlerApp.Jobs
{
    public class JobSchedule : BaseEntity
    {
        public DateTime ScheduledAt { get; init; } = DateTime.UtcNow;
        public DateTime Start { get; init; }
        public int LimitPerIteration { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
