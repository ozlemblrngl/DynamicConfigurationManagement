using DynamicConfigurationUI.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DynamicConfigurationUI.Controllers
{
	public class ConfigurationController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ConfigurationController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7146/api/Configurations");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<IEnumerable<ConfigurationListDto>>(jsonData);

				if (values != null)
				{
					return View(values);
				}
			}

			return View();
		}

		public async Task<IActionResult> GetByApplicationName(string applicationName)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7146/api/Configurations/{applicationName}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<IEnumerable<ConfigurationListDto>>(jsonData);

				if (values != null)
				{
					return View("Index", values);
				}
			}

			return View();
		}

		[HttpGet]
		public IActionResult AddConfiguration()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> AddConfiguration(CreateConfigurationDto createConfigurationDto)
		{

			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createConfigurationDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7146/api/Configurations", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();

		}

		public async Task<IActionResult> DeleteConfiguration(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:7146/api/Configurations/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet("Configuration/UpdateConfiguration/{id}")]
		public async Task<IActionResult> UpdateConfiguration(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7146/api/Configurations/Configuration/GetById/{id}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateConfigurationDto>(jsonData);
				return View(values);
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateConfiguration(UpdateConfigurationDto updateConfigurationDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateConfigurationDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var responseMessage = await client.PutAsync("https://localhost:7146/api/Configurations", stringContent);

			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View(updateConfigurationDto);
		}
	}
}
