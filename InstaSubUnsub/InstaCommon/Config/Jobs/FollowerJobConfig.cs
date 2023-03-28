using Microsoft.Extensions.Configuration;

namespace InstaCommon.Config.Jobs
{
    public class FollowerJobConfig : JobConfigBase
    {
        public FollowerJobConfig(IConfiguration config) : base(config)
        {
        }

        public override string SectionName => nameof(FollowerJobConfig);

        public double MinimumRank { get; set; } = 3.0;
        public int PostRecencyDays { get; set; } = 7;
        public bool HasRussianText { get; set; } = true;
        public bool IgnoreClosed { get; set; } = true;
    }
}
