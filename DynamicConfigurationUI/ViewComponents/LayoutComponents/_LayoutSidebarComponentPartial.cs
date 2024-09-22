using DynamicConfigurationUI.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace DynamicConfigurationUI.ViewComponents.LayoutComponents
{
	public class _LayoutSidebarComponentPartial : ViewComponent
	{
		private readonly IMemoryCache _cache;
		private readonly IHttpClientFactory _httpClientFactory;


		public _LayoutSidebarComponentPartial(IMemoryCache cache, IHttpClientFactory httpClientFactory)
		{
			_cache = cache;
			_httpClientFactory = httpClientFactory;
		}
		public IViewComponentResult Invoke()
		{
			const string cacheKey = "applicationListCache";
			if (!_cache.TryGetValue(cacheKey, out List<string> list))
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = client.GetAsync("https://localhost:7146/api/Configurations");

				if (responseMessage.Result.IsSuccessStatusCode)
				{
					var jsonData = responseMessage.Result.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<IEnumerable<ConfigurationListDto>>(jsonData.Result);

					if (values != null)
					{
						list = values.Select(x => x.ApplicationName).Distinct().ToList();
						_cache.Set(cacheKey, list, TimeSpan.FromMinutes(10)); // 10 dakika boyunca cache'de sakla
					}
				}
			}
			return View(list);
		}
	}
}
