using DynamicConfiguration.Dtos;
using DynamicConfiguration.Services.Concrete;
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

			return Ok(configs);
		}
		//[HttpGet]
		//[Route("File/{applicationName}")]
		////public async Task<IActionResult> GetConfigurationFile(string applicationName)
		//{
		//	var configs = await _configurationService.GetAllConfigurations();
		//	var activeConfigs = configs.Select(c => new ConfigurationListDto
		//	{
		//		Id = c.Id,
		//		Name = c.Name,
		//		Type = c.Type,
		//		Value = c.Value,
		//		IsActive = c.IsActive,
		//		ApplicationName = c.ApplicationName,

		//	});

		//	var result = new Dictionary<string, object>();

		//	foreach (var config in activeConfigs)
		//	{
		//		// Value'yu Type'a göre doğru tipe dönüştür
		//		object value;
		//		switch (config.Type.ToLower())
		//		{
		//			case "int":
		//				value = int.Parse(config.Value);
		//				break;
		//			case "bool":
		//				value = bool.Parse(config.Value);
		//				break;
		//			case "double":
		//				value = double.Parse(config.Value);
		//				break;
		//			case "string":
		//			default:
		//				value = config.Value;
		//				break;
		//		}

		//		// Name, Type'a göre değeri ekle
		//		result.Add(config.Name, value);
		//	}

		//	// JSON formatına dönüştür
		//	string json = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);

		//	// JSON olarak döndür
		//	return Content(json, "application/json");
		//}

		[HttpGet("Configuration/GetById/{id}")]
		public async Task<IActionResult> GetConfigurationById(int id)
		{
			var configs = await _configurationService.GetConfigurationById(id);
			return Ok(configs);
		}

		[HttpGet("{applicationName}")]
		public async Task<IActionResult> GetConfigurations(string applicationName)
		{
			var configs = await _configurationService.GetConfigurations(applicationName);
			return Ok(configs);
		}

		[HttpPost]
		public async Task<IActionResult> AddConfiguration([FromBody] CreateConfigurationDto createConfigurationdto)
		{
			if (ModelState.IsValid)
			{

				var result = await _configurationService.AddConfiguration(createConfigurationdto);
				return Ok(result);
			}

			return BadRequest(ModelState);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateConfiguration([FromBody] UpdateConfigurationDto updateConfigurationDto)
		{

			await _configurationService.UpdateConfiguration(updateConfigurationDto);

			return Ok(updateConfigurationDto);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteConfiguration(int id)
		{
			await _configurationService.DeleteConfiguration(id);
			return Ok();
		}





	}
}
