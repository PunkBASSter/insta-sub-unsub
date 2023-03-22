using Microsoft.Extensions.Configuration;

namespace InstaCommon.Config.Jobs
{
    public class UserCrawlerJobConfig : JobConfigBase
    {
        public UserCrawlerJobConfig(IConfiguration config) : base(config)
        {
        }

        public override string SectionName => nameof(UserCrawlerJobConfig);
    }
}
