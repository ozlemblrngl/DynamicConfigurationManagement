using DynamicConfiguration.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicConfiguration.Services
{
	public class ConfigurationService
	{


		private readonly ConfigurationDbContext _context;

		public ConfigurationService(ConfigurationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Configuration>> GetConfigurations(string applicationName)
		{
			return await _context.Configurations
				.Where(c => c.ApplicationName == applicationName && c.IsActive && !c.IsDeleted)
				.ToListAsync();
		}

		public async Task<Configuration> GetConfigurationById(int id)
		{
			return await _context.Configurations.FindAsync(id);
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
		public async Task<List<Configuration>> GetAllConfigurations()
		{
			return await _context.Configurations.Where(c => c.IsActive && !c.IsDeleted).ToListAsync();
		}

	}
}

