using Microsoft.AspNetCore.Mvc;
using Subsips_2.BusinessLogic.CoffeeCups;
using Subsips_2.BusinessLogic.Order;

namespace Subsips_2.Areas.Subsips.Controllers;

[Area("Subsips")]
public class CafeController : Controller
{
    private readonly ICoffeeCupRepository coffeeCups;
    private readonly IOrderRepository orderRepo;

    public CafeController(ICoffeeCupRepository coffeeCups, IOrderRepository orderRepo)
    {
        this.coffeeCups = coffeeCups;
        this.orderRepo = orderRepo;
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
            Console.WriteLine("Registered");// TODO WILL DO SOMETHING ;
        }


        return RedirectToAction("PhoneNumberRegister", "UserCustomer", new { coffeeId, orderId, cafeId });
    }
}
