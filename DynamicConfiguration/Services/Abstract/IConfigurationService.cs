using DynamicConfiguration.Models;

namespace DynamicConfiguration.Services.Abstract
{
	public interface IConfigurationService
	{
		Task<IEnumerable<Configuration>> GetConfigurations(string applicationName);
		Task<Configuration> GetConfigurationById(int id);
		Task<Configuration> GetConfigurationByName(string applicationName, string name);
		Task AddConfiguration(Configuration config);
		Task UpdateConfiguration(Configuration config);
		Task DeleteConfiguration(Configuration config);
		Task<IEnumerable<Configuration>> GetAllConfigurations();
	}

}
