using Business.Dtos;
using Entities;

namespace Business.Services.Abstract
{
	public interface IConfigurationService
	{
		Task<IEnumerable<ConfigurationListDto>> GetConfigurations(string applicationName);
		Task<ConfigurationDetailDto> GetConfigurationById(int id);
		Task<Configuration?> GetConfigurationByName(string name, string applicationName);
		Task<ConfigurationDetailDto> AddConfiguration(CreateConfigurationDto createConfigurationDto);
		Task UpdateConfiguration(UpdateConfigurationDto updateConfigurationDto);
		Task DeleteConfiguration(int id);
		Task<IEnumerable<ConfigurationListDto>> GetAllConfigurations();
	}

}
