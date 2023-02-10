using InstaDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InstaPersistence.EntityTypeConfig
{
    public class UserEntityConfig : IEntityTypeConfiguration<InstaUser>
    {
        public void Configure(EntityTypeBuilder<InstaUser> builder)
        {
            //builder.Ignore(e => e.DomainEvents);

            //builder.Property(t => t.Title)
            //    .HasMaxLength(200)
            //    .IsRequired();

            //builder.Property(p => p.Title).HasMaxLength(100).IsUnicode().IsRequired();
            //builder.Property(p => p.Description).IsUnicode().IsRequired();
            //price required?
        }
    }
}
