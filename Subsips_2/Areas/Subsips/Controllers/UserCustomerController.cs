using Microsoft.AspNetCore.Mvc;
using Subsips_2.Areas.Subsips.Models.UserCustomer;

namespace Subsips_2.Areas.Subsips.Controllers;

[Area("Subsips")]
public class UserCustomerController : Controller
{
    public IActionResult PhoneNumberRegister(Guid coffeeId, Guid orderId)
    {
        return View(new PhoneNumberRegisterViewModel
        {
            CoffeeId = coffeeId,
            OrderId = orderId
        });
    }
    [HttpPost]
    public IActionResult PhoneNumberRegister(PhoneNumberRegisterFormRequestModel formRequest)
    {
        return View();
    }
}
