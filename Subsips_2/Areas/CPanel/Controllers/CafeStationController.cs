using Microsoft.AspNetCore.Mvc;
using Subsips_2.Areas.CPanel.Models.CafeStation;
using Subsips_2.Areas.CPanel.Models.CafeStation.ModelView;
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

    public IActionResult TimeSheets()
    {
        var timeSheets = timeSheetShiftCafeRepository.GetTimeSheetsOfCafe(new Guid("B7C7D162-8871-4930-D55B-08DD08101897"));
        return View(new TimeSheetsCafeViewModel
        {
            Items = timeSheets.Result
        });
    }

    public IActionResult AddTimeSheet()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddTimeSheet([FromForm] TimeSheetShiftAddRequest request)
    {
        DateTime today = DateTime.Today;

        var StartDateTime = DateTime.Parse(request.StartTime).Add(today.TimeOfDay);
        var EndDateTime = DateTime.Parse(request.EndTime).Add(today.TimeOfDay);

        timeSheetShiftCafeRepository.Add(new Guid("B7C7D162-8871-4930-D55B-08DD08101897"), StartDateTime, EndDateTime, request.IsActive, request.PhoneNumber, request.DayOfWeek);


        return View();
    }

    public IActionResult DeleteTimeSheet(Guid id)
    {
        timeSheetShiftCafeRepository.Remove(id, new Guid("B7C7D162-8871-4930-D55B-08DD08101897"));
        return RedirectToAction("TimeSheets");
    }
    public IActionResult ChangeTimeSheet(Guid id)
    {
        timeSheetShiftCafeRepository.ChangeIsActiveState(id, new Guid("B7C7D162-8871-4930-D55B-08DD08101897"));
        return RedirectToAction("TimeSheets");
    }
}
