using InstaDomain;
using InstaPersistence.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InstaPersistence.EntityTypeConfig
{
    internal class KeyValueObjectConfig : IEntityTypeConfiguration<KeyValueJson>
    {
        public void Configure(EntityTypeBuilder<KeyValueJson> builder)
        {
            builder.Ignore(e => e.Id);
            builder.HasKey(e => e.Key);
            builder.Property(e => e.Value).HasColumnType("jsonb");
        }
    }
}
