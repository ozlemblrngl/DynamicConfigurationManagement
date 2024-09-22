
using Entities;

namespace DynamicConfigurationCore.Providers.Abstract
{
    public interface IConfigProvider
    {
        IEnumerable<Configuration> LoadConfigurations(string applicationName, string connectionString);
    }
}
