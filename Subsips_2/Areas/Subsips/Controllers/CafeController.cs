using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Subsips_2.Areas.Subsips.Models.Cafe;
using Subsips_2.BusinessLogic.Cafe;
using Subsips_2.BusinessLogic.CoffeeCups;
using Subsips_2.BusinessLogic.Order;
using Subsips_2.BusinessLogic.UserCustomer;

namespace Subsips_2.Areas.Subsips.Controllers;

[Area("Subsips")]
public class CafeController : Controller
{
    private readonly ICoffeeCupRepository coffeeCups;
    private readonly IOrderRepository orderRepo;
    private readonly IUserCustomerRepository customerRepo;
    private readonly ICafeStationRepository cafeRepo;
    private readonly ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegister;

    public CafeController(ICoffeeCupRepository coffeeCups, IOrderRepository orderRepo, ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegister, IUserCustomerRepository customerRepo, ICafeStationRepository cafeRepo)
    {
        this.coffeeCups = coffeeCups;
        this.orderRepo = orderRepo;
        this.customerPhoneRegister = customerPhoneRegister;
        this.customerRepo = customerRepo;
        this.cafeRepo = cafeRepo;
    }


    public IActionResult Menu(Guid Id)
    {
        var res = coffeeCups.GetAllBasedOnStation(Id);

        if (res is null || res.IsFailed)
            return NotFound();

        ViewData["OrderId"] = Guid.NewGuid().ToString();


        return View(res.Result);
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

        var regiseterRecordResult = customerPhoneRegister.Get(guidRid, cafeId);

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
