using Microsoft.AspNetCore.Mvc;

namespace DynamicConfigurationUI.ViewComponents.LayoutComponents
{
	public class _LayoutHeaderComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
