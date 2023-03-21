using InstaDomain;

namespace InstaPersistence.Utils
{
    public class KeyValueJson : BaseEntity
    {
        public string? Key { get; init; }

        public string? Value { get; set; }
    }
}
