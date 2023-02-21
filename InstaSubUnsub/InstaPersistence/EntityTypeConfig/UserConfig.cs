using InstaDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InstaPersistence.EntityTypeConfig
{
    internal class UserEntityConfig : IEntityTypeConfiguration<InstaUser>
    {
        public void Configure(EntityTypeBuilder<InstaUser> builder)
        {
            builder.HasKey(e => e.Id);//Property(p => p.Id).

            builder
                .HasIndex(e => e.Name)
                .IsUnique(true)
                    .IncludeProperties(p => new { p.IsFollower, p.Status });

            builder.Property(p => p.Name).HasMaxLength(30).IsRequired();

            //builder.HasMany<UserSubscription>(p => p.FollowingUsers).WithMany(p => p.Follower);
            //builder.Property(t => t.Title)
            //    .HasMaxLength(200)
            //    .IsRequired();

            //builder.Property(p => p.Title).HasMaxLength(100).IsUnicode().IsRequired();
            //builder.Property(p => p.Description).IsUnicode().IsRequired();
        }
    }
}
