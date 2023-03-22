using Microsoft.Extensions.Configuration;

namespace InstaCommon.Config.Jobs
{
    public class FollowerJobConfig : JobConfigBase
    {
        public FollowerJobConfig(IConfiguration config) : base(config)
        {
        }

        public override string SectionName => nameof(FollowerJobConfig);
    }
}
