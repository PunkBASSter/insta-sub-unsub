using Microsoft.Extensions.Configuration;
namespace InstaCommon.Config
{
    /// <summary>
    /// Reads the latest actual config values on object creation.
    /// </summary>
    public abstract class RefreshableConfigBase //: IValidatableObject
    {
        private readonly IConfiguration _configuration;

        public IConfiguration Expose() { return _configuration; }

        public abstract string SectionName { get; }

        public RefreshableConfigBase(IConfiguration config)
        {
            _configuration = config;
            config.GetRequiredSection(SectionName).Bind(this);
        }
    }
}
