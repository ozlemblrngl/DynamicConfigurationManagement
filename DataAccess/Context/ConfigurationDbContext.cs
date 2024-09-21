using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
	public class ConfigurationDbContext : DbContext
	{
		public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options) : base(options) { }


		public DbSet<Configuration> Configurations { get; set; }
	}
}
