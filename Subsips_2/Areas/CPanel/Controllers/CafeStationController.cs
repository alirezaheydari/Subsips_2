using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subsips_2.Areas.CPanel.Models.CafeStation;
using Subsips_2.Areas.CPanel.Models.CafeStation.ModelView;
using Subsips_2.BusinessLogic.Cafe;
using System.Security.Claims;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("Cpanel")]
public class CafeStationController : Controller
{
    private readonly ITimeSheetShiftCafeRepository timeSheetShiftCafeRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ICafeStationAspNetUserRepository cafeStationAspNetUserRepository;
    public CafeStationController(ITimeSheetShiftCafeRepository timeSheetShiftCafeRepository, IHttpContextAccessor httpContextAccessor, ICafeStationAspNetUserRepository cafeStationAspNetUserRepository)
    {
        this.timeSheetShiftCafeRepository = timeSheetShiftCafeRepository;
        //new Guid("B7C7D162-8871-4930-D55B-08DD08101897");
        this.httpContextAccessor = httpContextAccessor;
        this.cafeStationAspNetUserRepository = cafeStationAspNetUserRepository;
    }
    public IActionResult TimeSheets()
    {

        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();

        var timeSheets = timeSheetShiftCafeRepository.GetTimeSheetsOfCafe(cafeId);
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

        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();



        DateTime today = DateTime.Today;

        var StartDateTime = DateTime.Parse(request.StartTime).Add(today.TimeOfDay);
        var EndDateTime = DateTime.Parse(request.EndTime).Add(today.TimeOfDay);

        timeSheetShiftCafeRepository.Add(cafeId, StartDateTime, EndDateTime, request.IsActive, request.PhoneNumber, request.DayOfWeek);


        return View();
    }

    public IActionResult DeleteTimeSheet(Guid id)
    {
        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();

        timeSheetShiftCafeRepository.Remove(id, cafeId);
        return RedirectToAction("TimeSheets");
    }
    public IActionResult ChangeTimeSheet(Guid id)
    {
        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();

        timeSheetShiftCafeRepository.ChangeIsActiveState(id, cafeId);
        return RedirectToAction("TimeSheets");
    }
}
