﻿using Microsoft.Extensions.Configuration;

namespace InstaCommon.Config.Jobs
{
    public class UnfollowerJobConfig : JobConfigBase
    {
        public UnfollowerJobConfig(IConfiguration config) : base(config)
        {
        }

        public override string SectionName => nameof(UnfollowerJobConfig);
    }
}
