using Business.Dtos;
using Business.Services.Concrete;
using Microsoft.AspNetCore.Mvc;


namespace DynamicConfigurationWebApi.Controllers
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
