using Microsoft.EntityFrameworkCore;

using Metro.Models;
using Metro.Data.EntityConfiguration;

namespace Metro.Data
{
    public class MetroDbContext :DbContext
    {
        public MetroDbContext()
        {
        }

        public MetroDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        public DbSet<Station> Stations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new StationConfiguration());
        }
    }
}
