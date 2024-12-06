using Microsoft.AspNetCore.Mvc;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("CPanel")]
public class SubwayStationController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Add()
    {
        return View();
    }
}
