using DynamicConfigurationCore.Providers.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicConfigurationCore.Providers.Concrete
{
    public class MssqlConfigurationProvider : IConfigProvider
    {
        public IEnumerable<Configuration> LoadConfigurations(string applicationName, string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DynamicConfigurationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            using (var dbContext = new DynamicConfigurationDbContext(optionsBuilder.Options))
            {
                return dbContext.Configurations.ToList();
            }
        }
    }
}
