using DynamicConfiguration.Dtos;
using DynamicConfiguration.Models;

namespace DynamicConfiguration.Services.Abstract
{
	public interface IConfigurationService
	{
		Task<IEnumerable<ConfigurationListDto>> GetConfigurations(string applicationName);
		Task<ConfigurationDetailDto> GetConfigurationById(int id);
		Task<Configuration?> GetConfigurationByName(string name, string applicationName);
		Task AddConfiguration(CreateConfigurationDto createConfigurationDto);
		Task UpdateConfiguration(UpdateConfigurationDto updateConfigurationDto);
		Task DeleteConfiguration(int id);
		Task<IEnumerable<ConfigurationListDto>> GetAllConfigurations();
	}

}
