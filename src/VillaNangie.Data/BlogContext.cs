using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VillaNangie.Data.Models;

namespace VillaNangie.Data
{
    public class BlogContext : IdentityDbContext<BlogUser>
    {
        private IConfigurationRoot _config;

        public BlogContext(DbContextOptions<BlogContext> options, IConfigurationRoot config) : base(options)
        {
            _config = config;
        }

        public DbSet<BlogStory> Stories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:BlogDb"]);
            base.OnConfiguring(optionsBuilder);
        }

    }
}
