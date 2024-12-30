using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Helper.Validations;
using Subsips_2.Areas.CPanel.Models.CafeStation.FormRequest;
using Subsips_2.Areas.Subsips.Models.Cafe;
using Subsips_2.BusinessLogic.Cafe;
using Subsips_2.BusinessLogic.CafeConfig;
using Subsips_2.BusinessLogic.CoffeeCups;
using Subsips_2.BusinessLogic.ExclusiveCustomer;
using Subsips_2.BusinessLogic.Order;
using Subsips_2.BusinessLogic.SendNotification;
using Subsips_2.BusinessLogic.UserCustomer;
using System.Security.Claims;

namespace Subsips_2.Areas.Subsips.Controllers;

[Area("Subsips")]
public class CafeController : Controller
{
    private readonly ICoffeeCupRepository coffeeCups;
    private readonly IUserCustomerRepository customerRepo;
    private readonly ICafeStationRepository cafeRepo;
    private readonly ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegister;
    private readonly IExclusiveCafeCustomerRepository exclusiveCafeCustomerRepository;
    private readonly ICafeStationConfigRepository cafeStationConfigRepository;
    private readonly ISendSmsNotification smsSender;

    public CafeController(ICoffeeCupRepository coffeeCups, ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegister, IUserCustomerRepository customerRepo, ICafeStationRepository cafeRepo, IExclusiveCafeCustomerRepository exclusiveCafeCustomerRepository, ICafeStationConfigRepository cafeStationConfigRepository, ISendSmsNotification smsSender)
    {
        this.coffeeCups = coffeeCups;
        this.customerPhoneRegister = customerPhoneRegister;
        this.customerRepo = customerRepo;
        this.cafeRepo = cafeRepo;
        this.exclusiveCafeCustomerRepository = exclusiveCafeCustomerRepository;
        this.cafeStationConfigRepository = cafeStationConfigRepository;
        this.smsSender = smsSender;
    }


    public IActionResult Menu(Guid Id)
    {
        var res = coffeeCups.GetAllBasedOnStation(Id);

        if (res is null || res.IsFailed)
            return NotFound();

        ViewData["OrderId"] = Guid.NewGuid().ToString();


        return View(res.Result);
    }

    public IActionResult ExclusiveUser(Guid Id)
    {
        var cafe = cafeRepo.Find(Id);

        if (cafe.IsFailed)
        {
            ViewData["ErrorValidation"] = "مشکلی پیش آمده";
            return View();
        }

        ViewData["CafeId"] = cafe.Result.Id;

        ViewData["CafeName"] = cafe.Result.Name;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ExclusiveUser(ExclusiveUserFormRequest formRequest)
    {
        formRequest.PhoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(formRequest.PhoneNumber);

        if (!PhoneNumberValidation.IsValid(formRequest.PhoneNumber))
        {
            ViewData["ErrorValidation"] = "لطفا شماره همراه را به درستی وارد نمایید";
            return View();
        }

        var cafe = cafeRepo.Find(formRequest.CafeId);

        if (cafe.IsFailed)
        {
            ViewData["ErrorValidation"] = "مشکلی پیش آمده";
            return View();
        }

        await exclusiveCafeCustomerRepository.Add(formRequest.CafeId, formRequest.PhoneNumber, formRequest.FullName);
        var config = cafeStationConfigRepository.FindActiveByCafeId(formRequest.CafeId);
        if (config is null && config.IsFailed)
            return RedirectToAction("Index", "Home");

        if (config.Result.IsSendSms)
        {
            smsSender.SendDefaultMsgForCafe(formRequest.PhoneNumber, config.Result.DefaultSmsMsg);
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Select(Guid coffeeId, Guid orderId, Guid cafeId)
    {
        if (coffeeId == Guid.Empty && orderId == Guid.Empty && cafeId == Guid.Empty)
            return NotFound();
        var request = ControllerContext?.HttpContext?.Request;
        var isRegistered = request == null ? false : request?.Cookies?.Any(x => x.Key == "RID") ?? false;

        if (isRegistered)
            return RedirectToAction("ConfirmedOrder", new { coffeeId, orderId, cafeId });


        return RedirectToAction("PhoneNumberRegister", "UserCustomer", new { coffeeId, orderId, cafeId });
    }

    public IActionResult ConfirmedOrder(Guid coffeeId, Guid orderId, Guid cafeId)
    {
        if (coffeeId == Guid.Empty && orderId == Guid.Empty && cafeId == Guid.Empty)
            return NotFound();
        var request = ControllerContext?.HttpContext?.Request;
        var isRegistered = request == null ? false : request?.Cookies?.Any(x => x.Key == "RID") ?? false;
        if (!isRegistered)
        {
            return RedirectToAction("PhoneNumberRegister", "UserCustomer", new { coffeeId, orderId, cafeId });
        }

        var rid = request?.Cookies?.FirstOrDefault(x => x.Key == "RID").Value;

        if (rid.IsNullOrEmpty())
            return RedirectToAction("PhoneNumberRegister", "UserCustomer", new { coffeeId, orderId, cafeId });

        var guidRid = new Guid(rid);

        var regiseterRecordResult = customerPhoneRegister.Get(guidRid);

        if (regiseterRecordResult is null || regiseterRecordResult.IsFailed)
            return RedirectToAction("PhoneNumberRegister", "UserCustomer", new { coffeeId, orderId, cafeId });

        var regiseterRecord = regiseterRecordResult.Result;

        var currentCustomerResult = customerRepo.Find(regiseterRecord.UserCustomerId);

        if (currentCustomerResult is null || currentCustomerResult.IsFailed)
            return RedirectToAction("PhoneNumberRegister", "UserCustomer", new { coffeeId, orderId, cafeId });

        var coffeeResult = coffeeCups.FindCoffeeAndCafeInfo(coffeeId);

        if (coffeeResult is null || coffeeResult.IsFailed)
            return NotFound();



        return View(new Subsips_2.Areas.Subsips.Models.Cafe.ConfirmedOrderModelView
        {
            cafeId = cafeId,
            coffeeId = coffeeId,
            orderId = orderId,
            CustomerFullName = currentCustomerResult.Result.FullName,
            CustomerPhoneNumber = currentCustomerResult.Result.PhoneNumber,
            CafeName = coffeeResult.Result.CafeName,
            CoffeeName = coffeeResult.Result.CoffeeName,
            StationName = coffeeResult.Result.StationName,
            CoffeePrice = coffeeResult.Result.Price
        });
    }

}
