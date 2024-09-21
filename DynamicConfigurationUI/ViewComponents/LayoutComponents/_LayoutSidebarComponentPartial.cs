using Microsoft.AspNetCore.Mvc;

namespace DynamicConfigurationUI.ViewComponents.LayoutComponents
{
	public class _LayoutSidebarComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
