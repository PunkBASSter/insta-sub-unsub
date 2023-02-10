using InstaDomain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InstaPersistence
{
    public class InstaDbContext : DbContext
    {
        public InstaDbContext() { }

        public InstaDbContext(DbContextOptions<InstaDbContext> options) : base(options) { }

        public DbSet<InstaUser> InstaUsers { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True");
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}