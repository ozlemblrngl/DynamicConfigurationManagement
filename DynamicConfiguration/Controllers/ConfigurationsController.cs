using DynamicConfiguration.Dtos;
using DynamicConfiguration.Models;
using DynamicConfiguration.Services;
using Microsoft.AspNetCore.Mvc;


namespace DynamicConfiguration.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigurationsController : ControllerBase
	{
		private readonly ConfigurationService _configurationService;

		public ConfigurationsController(ConfigurationService configurationService)
		{
			_configurationService = configurationService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllConfigurations()
		{
			var configs = await _configurationService.GetAllConfigurations();
			var result = configs.Select(c => new ConfigurationListDto
			{
				Id = c.Id,
				Name = c.Name,
				Type = c.Type,
				Value = c.Value,
				IsActive = c.IsActive,
				ApplicationName = c.ApplicationName,
				IsDeleted = c.IsDeleted
			});

			return Ok(result);
		}


		[HttpGet("{applicationName}")]
		public async Task<IActionResult> GetConfigurations(string applicationName)
		{
			var configs = await _configurationService.GetConfigurations(applicationName);
			var result = configs.Select(c => new ConfigurationListDto
			{
				Id = c.Id,
				Name = c.Name,
				Type = c.Type,
				Value = c.Value,
				IsActive = c.IsActive,
				ApplicationName = c.ApplicationName,
				IsDeleted = c.IsDeleted
			});

			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddConfiguration([FromBody] CreateConfigurationDto dto)
		{
			if (ModelState.IsValid)
			{
				var config = new Configuration
				{
					Name = dto.Name,
					Type = dto.Type,
					Value = dto.Value,
					IsActive = dto.IsActive,
					ApplicationName = dto.ApplicationName
				};

				await _configurationService.AddConfiguration(config);
				return Ok(config);
			}

			return BadRequest(ModelState);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateConfiguration(int id, [FromBody] UpdateConfigurationDto dto)
		{
			var config = await _configurationService.GetConfigurationById(id);
			if (config == null)
				return NotFound();

			config.Name = dto.Name;
			config.Type = dto.Type;
			config.Value = dto.Value;
			config.IsActive = dto.IsActive;
			config.ApplicationName = dto.ApplicationName;

			await _configurationService.UpdateConfiguration(config);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteConfiguration(int id)
		{
			var config = await _configurationService.GetConfigurationById(id);
			if (config == null)
				return NotFound();

			await _configurationService.DeleteConfiguration(config);
			return NoContent();
		}





	}
}
