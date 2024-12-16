using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subsips_2.BusinessLogic.SubwayStation;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("CPanel")]
[Authorize]
public class SubwayStationController : Controller
{
    private readonly ISubwayStationRepository subwayStation;

    public SubwayStationController(ISubwayStationRepository subwayStation)
    {
        this.subwayStation = subwayStation;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(string name)
    {

        return View();
    }
}
