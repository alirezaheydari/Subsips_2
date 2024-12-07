using Microsoft.AspNetCore.Mvc;
using Subsips_2.Areas.Subsips.Models.SubwayStation;
using Subsips_2.BusinessLogic.SubwayStation;

namespace Subsips_2.Areas.Subsips.Controllers;

[Area("Subsips")]
public class HomeController : Controller
{
    private readonly ISubwayStationRepository subwayStation;

    public HomeController(ISubwayStationRepository subwayStation)
    {
        this.subwayStation = subwayStation;
    }
    public IActionResult Index()
    {
        var stations = subwayStation.GetViewModelAll();
        if (stations.IsFailed)
            return View("Error");

        return View(stations.Result);
    }
}
