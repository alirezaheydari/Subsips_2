using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Helper.Validations;
using Subsips_2.Areas.CPanel.Models.CafeStation;
using Subsips_2.Areas.CPanel.Models.CafeStation.FormRequest;
using Subsips_2.Areas.CPanel.Models.CafeStation.ModelView;
using Subsips_2.BusinessLogic.Cafe;
using Subsips_2.BusinessLogic.CafeConfig;
using Subsips_2.BusinessLogic.ExclusiveCustomer;
using System.Security.Claims;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("Cpanel")]
[Authorize]
public class CafeStationController : Controller
{
    private readonly ITimeSheetShiftCafeRepository timeSheetShiftCafeRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ICafeStationAspNetUserRepository cafeStationAspNetUserRepository;
    private readonly IExclusiveCafeCustomerRepository exclusiveCafeCustomerRepository;
    private readonly ICafeStationConfigRepository cafeStationConfigRepository;
    public CafeStationController(ITimeSheetShiftCafeRepository timeSheetShiftCafeRepository, IHttpContextAccessor httpContextAccessor, ICafeStationAspNetUserRepository cafeStationAspNetUserRepository, IExclusiveCafeCustomerRepository exclusiveCafeCustomerRepository, ICafeStationConfigRepository cafeStationConfigRepository)
    {
        this.timeSheetShiftCafeRepository = timeSheetShiftCafeRepository;
        //new Guid("B7C7D162-8871-4930-D55B-08DD08101897");
        this.httpContextAccessor = httpContextAccessor;
        this.cafeStationAspNetUserRepository = cafeStationAspNetUserRepository;
        this.exclusiveCafeCustomerRepository = exclusiveCafeCustomerRepository;
        this.cafeStationConfigRepository = cafeStationConfigRepository;
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


    public IActionResult ExclusiveUsers()
    {
        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();


        var model = exclusiveCafeCustomerRepository.GetExclusive(cafeId);
        return View(new ExclusiveUsersModelView
        {
            Items = model.Result ?? new List<ExclusiveUsersItemModelView>()
        });
    }
    public IActionResult AddExclusiveUser()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> AddExclusiveUser(ExclusiveUserFormRequest formRequest)
    {
        formRequest.PhoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(formRequest.PhoneNumber);

        if (!PhoneNumberValidation.IsValid(formRequest.PhoneNumber))
        {
            ViewData["ErrorValidation"] = "لطفا شماره همراه را به درستی وارد نمایید";
            return View();
        }

        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();


        await exclusiveCafeCustomerRepository.Add(cafeId, formRequest.PhoneNumber, formRequest.FullName);


        return RedirectToAction("ExclusiveUsers");
    }

    public IActionResult CafeConfig()
    {
        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();

        var res = cafeStationConfigRepository.FindByCafeId(cafeId);
        if (res is null || res.IsFailed)
            return View();

        return View(new CafeConfigFormRequest
        {
            IsActive = res.Result.IsActive,
            IsSendSms = res.Result.IsSendSms,
            DefaultMsg = res.Result.DefaultSmsMsg,
        });

    }
    [HttpPost]
    public async Task<IActionResult> CafeConfig([FromForm] CafeConfigFormRequest formRequest)
    {
        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();

        await cafeStationConfigRepository.Add(cafeId, formRequest.DefaultMsg?.Trim() ?? string.Empty, formRequest.IsSendSms, formRequest.IsActive);

        return RedirectToAction("ExclusiveUsers");
    }
}
