using Microsoft.Extensions.Configuration;

namespace InstaCommon.Config.Jobs
{
    public class InstaDbSyncherJobConfig : JobConfigBase
    {
        public InstaDbSyncherJobConfig(IConfiguration config) : base(config)
        {
        }

        public override string SectionName => nameof(InstaDbSyncherJobConfig);
    }
}
