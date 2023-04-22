using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace InstaCommon.Config.Jobs
{
    /// <summary>
    /// Children should contain the actual job Type Name.
    /// </summary>
    public abstract class JobConfigBase : RefreshableConfigBase
    {
        protected JobConfigBase(IConfiguration config) : base(config)
        {
        }

        public bool Disabled { get; set; } = false;
        public int LimitPerDay { get; set; }
        public int LimitPerDayDispersion { get; set; }
        public int LimitPerIteration { get; set; }
        public int LimitPerIterationDispersion { get; set; }
        public string? CronSchedule { get; set; }

        [Required]
        public int? WorkStartingHour { get; set; }

        [Required]
        public int? WorkDurationHours { get; set; }
        public int MinDelay { get; set; }
        public int MaxDelay { get; set; }
        public int MinIterationsPerDay { get; set; }
        public int MaxIterationsPerDay { get; set; }
        public int MaxIntervalDispersion { get; set; }
    }
}
