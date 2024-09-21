using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Context
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
	{
		public ConfigurationDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();

			optionsBuilder.UseSqlServer(@"Server=DESKTOP-N9UK4PN;Database=ConfigurationDb;Trusted_Connection=true;TrustServerCertificate=true");

			return new ConfigurationDbContext(optionsBuilder.Options);
		}
	}
}
