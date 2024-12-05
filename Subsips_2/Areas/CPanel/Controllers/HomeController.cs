using Microsoft.AspNetCore.Mvc;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("CPanel")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
