using DynamicConfiguration.Models;
using DynamicConfiguration.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DynamicConfiguration.Services.Concrete
{
	public class ConfigurationService : IConfigurationService
	{


		private readonly ConfigurationDbContext _context;


		public ConfigurationService(ConfigurationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Configuration>> GetConfigurations(string applicationName)
		{
			return await _context.Configurations
				.Where(c => c.ApplicationName == applicationName && c.IsActive)
				.ToListAsync();
		}

		public async Task<Configuration> GetConfigurationById(int id)
		{
			return await _context.Configurations.FindAsync(id);
		}

		public async Task<Configuration?> GetConfigurationByName(string name, string applicationName)
		{
			return await _context.Configurations
				.Where(c => c.ApplicationName == applicationName && c.Name == name && c.IsActive).FirstOrDefaultAsync();
		}
		public async Task AddConfiguration(Configuration config)
		{
			_context.Configurations.Add(config);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateConfiguration(Configuration config)
		{
			_context.Configurations.Update(config);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteConfiguration(Configuration config)
		{
			config.IsDeleted = true;
			await _context.SaveChangesAsync();
		}
		public async Task<IEnumerable<Configuration>> GetAllConfigurations()
		{
			return await _context.Configurations.Where(c => c.IsActive).ToListAsync();
		}

	}
}

