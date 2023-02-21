﻿using InstaDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InstaPersistence.EntityTypeConfig
{
    internal class UserSubscriptionEntityConfig : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.Ignore(e => e.Id);
            builder.HasKey(e => new { e.FollowerId, e.FolloweeId });

            builder.HasOne(p => p.Follower)
                .WithMany(p => p.Followees)
                .HasForeignKey(e => e.FollowerId);

            builder.HasOne(e => e.Followee)
                .WithMany(p => p.Followers)
                .HasForeignKey(e => e.FolloweeId);

            //builder
            //    .HasIndex(e => new { e.Follower, e.Followee })
            //    .IsUnique();
        }
    }
}
