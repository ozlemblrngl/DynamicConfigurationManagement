using AutoMapper;
using DynamicConfiguration.Dtos;
using DynamicConfiguration.Models;
using DynamicConfiguration.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DynamicConfiguration.Services.Concrete
{
	public class ConfigurationService : IConfigurationService
	{


		private readonly ConfigurationDbContext _context;
		private readonly IMapper _mapper;

		public ConfigurationService(ConfigurationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ConfigurationListDto>> GetConfigurations(string applicationName)
		{
			var configs = await _context.Configurations
				.Where(c => c.ApplicationName == applicationName && c.IsActive)
				.ToListAsync();

			return _mapper.Map<IEnumerable<ConfigurationListDto>>(configs);
		}

		public async Task<ConfigurationDetailDto> GetConfigurationById(int id)
		{
			var config = await _context.Configurations.FindAsync(id);
			return _mapper.Map<ConfigurationDetailDto>(config);
		}

		public async Task<Configuration?> GetConfigurationByName(string name, string applicationName)
		{
			return await _context.Configurations
				.Where(c => c.ApplicationName == applicationName && c.Name == name && c.IsActive)
				.FirstOrDefaultAsync();
		}
		public async Task AddConfiguration(CreateConfigurationDto createConfigurationDto)
		{
			var config = _mapper.Map<Configuration>(createConfigurationDto);

			_context.Configurations.Add(config);
			await _context.SaveChangesAsync();
		}


		public async Task UpdateConfiguration(UpdateConfigurationDto updateConfigurationDto)
		{
			var config = await _context.Configurations.FindAsync(updateConfigurationDto.Id);

			_mapper.Map(updateConfigurationDto, config);
			await _context.SaveChangesAsync();
		}


		public async Task DeleteConfiguration(int id)
		{
			var config = await _context.Configurations.Where(c => c.Id == id).FirstOrDefaultAsync();
			config.IsActive = false;
			await _context.SaveChangesAsync();
		}
		public async Task<IEnumerable<ConfigurationListDto>> GetAllConfigurations()
		{
			var configurations = await _context.Configurations
				.Where(c => c.IsActive)
				.ToListAsync();
			return _mapper.Map<IEnumerable<ConfigurationListDto>>(configurations);
		}


	}
}

