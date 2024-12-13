using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Subsips_2.BusinessLogic.CoffeeCups;
using Subsips_2.BusinessLogic.Order;
using Subsips_2.BusinessLogic.UserCustomer;
using System.Net.WebSockets;

namespace Subsips_2.Areas.Subsips.Controllers;

[Area("Subsips")]
public class CafeController : Controller
{
    private readonly ICoffeeCupRepository coffeeCups;
    private readonly IOrderRepository orderRepo;
    private readonly IUserCustomerRepository customerRepo;
    private readonly ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegister;

    public CafeController(ICoffeeCupRepository coffeeCups, IOrderRepository orderRepo, ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegister, IUserCustomerRepository customerRepo)
    {
        this.coffeeCups = coffeeCups;
        this.orderRepo = orderRepo;
        this.customerPhoneRegister = customerPhoneRegister;
        this.customerRepo = customerRepo;
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
        {

            var rid = request?.Cookies?.FirstOrDefault(x => x.Key == "RID").Value;

            if (rid.IsNullOrEmpty())
                return NotFound(); // TODO : change 

            var guidRid = new Guid(rid);

            var regiseterRecordResult = customerPhoneRegister.Get(guidRid, cafeId);
            
            if (regiseterRecordResult is null || regiseterRecordResult.IsFailed)
                return NotFound();
            // TODO : 1- getUser 

            var regiseterRecord = regiseterRecordResult.Result;

            var currentCustomerResult = customerRepo.Find(regiseterRecord.UserCustomerId);

            if (currentCustomerResult is null || currentCustomerResult.IsFailed)
                return NotFound();

            // TODO : 2- make order 


            var currnetCustomer = currentCustomerResult.Result;

            orderRepo.MakeNewOrder(orderId, string.Empty, cafeId, coffeeId, currnetCustomer.Id);

            // TODO : 3- show status

            return RedirectToAction("ShowStatusOrder", "UserCustomer", new { orderId = orderId });

        }


        return RedirectToAction("PhoneNumberRegister", "UserCustomer", new { coffeeId, orderId, cafeId });
    }
}
