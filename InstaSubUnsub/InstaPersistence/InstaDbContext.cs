using InstaDomain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InstaPersistence
{
    public class InstaDbContext : DbContext
    {
        public InstaDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO take connection string from config
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=insta_subs;Username=insta_service;Password=insta_service");
        }
        
        public DbSet<InstaUser> InstaUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}