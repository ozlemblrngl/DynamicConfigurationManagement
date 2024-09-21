using DynamicConfiguration.Dtos;
using DynamicConfiguration.Models;
using DynamicConfiguration.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


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
		[HttpGet]
		[Route("File/{applicationName}")]
		public async Task<IActionResult> GetConfigurationFile(string applicationName)
		{
			var configs = await _configurationService.GetAllConfigurations();
			var activeConfigs = configs.Select(c => new ConfigurationListDto
			{
				Id = c.Id,
				Name = c.Name,
				Type = c.Type,
				Value = c.Value,
				IsActive = c.IsActive,
				ApplicationName = c.ApplicationName,
				IsDeleted = c.IsDeleted
			});

			var result = new Dictionary<string, object>();

			foreach (var config in activeConfigs)
			{
				// Value'yu Type'a göre doğru tipe dönüştür
				object value;
				switch (config.Type.ToLower())
				{
					case "int":
						value = int.Parse(config.Value);
						break;
					case "bool":
						value = bool.Parse(config.Value);
						break;
					case "double":
						value = double.Parse(config.Value);
						break;
					case "string":
					default:
						value = config.Value;
						break;
				}

				// Name, Type'a göre değeri ekle
				result.Add(config.Name, value);
			}

			// JSON formatına dönüştür
			string json = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);

			// JSON olarak döndür
			return Content(json, "application/json");
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
