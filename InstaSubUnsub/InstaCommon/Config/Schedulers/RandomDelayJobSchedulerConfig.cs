using Microsoft.Extensions.Configuration;

namespace InstaCommon.Config.Schedulers
{
    public class RandomDelayJobSchedulerConfig : RefreshableConfigBase
    {
        public RandomDelayJobSchedulerConfig(IConfiguration config) : base(config)
        {
        }

        public override string SectionName => nameof(RandomDelayJobSchedulerConfig);

        public int MinDelay { get; set; }
        public int MaxDelay { get; set; }
    }
}
