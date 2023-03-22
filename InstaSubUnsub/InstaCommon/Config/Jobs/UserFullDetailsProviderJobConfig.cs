using Microsoft.Extensions.Configuration;

namespace InstaCommon.Config.Jobs
{
    public class UserFullDetailsProviderJobConfig : JobConfigBase
    {
        public UserFullDetailsProviderJobConfig(IConfiguration config) : base(config)
        {
        }

        public override string SectionName => nameof(UserFullDetailsProviderJobConfig);
    }
}
