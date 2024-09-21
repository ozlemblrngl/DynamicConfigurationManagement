using Microsoft.AspNetCore.Mvc;

namespace DynamicConfigurationUI.Controllers
{
	public class ConfigurationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
