using Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicConfigurationCore
{
    public class DynamicConfigurationDbContext : DbContext
    {
        public DynamicConfigurationDbContext(DbContextOptions<DynamicConfigurationDbContext> options) : base(options)
        {

        }

        public DbSet<Configuration> Configurations { get; set; }
    }
}
