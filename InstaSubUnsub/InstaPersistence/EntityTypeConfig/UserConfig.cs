using InstaDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InstaPersistence.EntityTypeConfig
{
    internal class UserEntityConfig : IEntityTypeConfiguration<InstaUser>
    {
        public void Configure(EntityTypeBuilder<InstaUser> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .HasIndex(e => e.Name)
                .IsUnique(true)
                    .IncludeProperties(p => new { p.IsFollower, p.Status, p.Rank });

            builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
        }
    }
}
