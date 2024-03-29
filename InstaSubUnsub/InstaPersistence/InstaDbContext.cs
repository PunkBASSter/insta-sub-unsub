﻿using InstaCrawlerApp.Scheduling;
using InstaDomain;
using InstaDomain.Account;
using InstaPersistence.DataSeed;
using InstaPersistence.Utils;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InstaPersistence
{
    public class InstaDbContext : DbContext
    {
        public InstaDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<InstaUser> InstaUsers { get; set; }

        public DbSet<UserRelation> UserRelations { get; set; }

        public DbSet<JobExecutionDetails> JobExecutionDetails { get; set; }

        public DbSet<KeyValueJson> KeyValueJsonObjects { get; set; }

        public DbSet<AccountUsageHistory> ServiceAccountsHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<InstaUser>().HasData(new ProtectedInstaUsers().GetSeedData());

            base.OnModelCreating(builder);
        }
    }
}