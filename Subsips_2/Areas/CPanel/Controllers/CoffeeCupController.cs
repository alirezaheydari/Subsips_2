using Microsoft.AspNetCore.Mvc;
using Subsips_2.Areas.CPanel.Models.CoffeeCup;
using Subsips_2.BusinessLogic.CoffeeCups;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("Cpanel")]
public class CoffeeCupController : Controller
{
    private readonly ICoffeeCupRepository coffeeCup;

    public CoffeeCupController(ICoffeeCupRepository coffeeCup)
    {
        this.coffeeCup = coffeeCup;
    }


    public IActionResult Index()
    {
        var resultOfCoffee = coffeeCup.GetAllBasedOnCafe(new Guid("B7C7D162-8871-4930-D55B-08DD08101897"));
        if (resultOfCoffee.IsFailed)
            return NotFound();
        return View(resultOfCoffee.Result);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm] AddCoffeeCupRequestModel request)
    {
        coffeeCup.Add(request.Name, new Guid("B7C7D162-8871-4930-D55B-08DD08101897"), request.Price, request.Description, string.Empty);
        return View();
    }
}
