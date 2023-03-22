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

        [Required]
        public int LimitPerIteration { get; set; }
        public int LimitDispersion { get; set; }
        public string? CronSchedule { get; set; }
        public int? WorkStartingHour { get; set; }
        public int? WorkDurationHours { get; set; }
    }
}
