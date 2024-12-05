using Microsoft.AspNetCore.Mvc;

namespace Subsips_2.Areas.Subsips.Controllers;

[Area("Subsips")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
