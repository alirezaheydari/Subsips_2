using Microsoft.AspNetCore.Mvc;
using Subsips_2.BusinessLogic.Cafe;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("Cpanel")]
public class CafeStationController : Controller
{
    private readonly ITimeSheetShiftCafeRepository timeSheetShiftCafeRepository;

    public CafeStationController(ITimeSheetShiftCafeRepository timeSheetShiftCafeRepository)
    {
        this.timeSheetShiftCafeRepository = timeSheetShiftCafeRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Add()
    {
        return View();
    }
}
