using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InstaPersistence
{
    internal class InstaDbContextDesignTimeFactory : IDesignTimeDbContextFactory<InstaDbContext>
    {
        public InstaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InstaDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=insta_subs;Username=insta_service;Password=insta_service");

            return new InstaDbContext(optionsBuilder.Options);
        }
    }
}
