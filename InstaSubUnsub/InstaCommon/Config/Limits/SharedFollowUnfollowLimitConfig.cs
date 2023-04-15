using Microsoft.Extensions.Configuration;

namespace InstaCommon.Config.Limits
{
    public class SharedFollowUnfollowLimitConfig : RefreshableConfigBase
    {
        public SharedFollowUnfollowLimitConfig(IConfiguration config) : base(config)
        {
        }

        public override string SectionName => nameof(SharedFollowUnfollowLimitConfig);

        public int LimitPerHour { get; set; }
        
        public ICollection<string> JobTypeNames { get; set; } = new string[] { "Follower", "Unfollower" };
    }
}
