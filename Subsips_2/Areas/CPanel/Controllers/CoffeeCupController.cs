using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subsips_2.Areas.CPanel.Models.CoffeeCup;
using Subsips_2.BusinessLogic.Cafe;
using Subsips_2.BusinessLogic.CoffeeCups;
using System.Security.Claims;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("Cpanel")]
[Authorize]
public class CoffeeCupController : Controller
{
    private readonly ICoffeeCupRepository coffeeCup;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ICafeStationAspNetUserRepository cafeStationAspNetUserRepository;
    //private readonly Guid currentCafeId;

    public CoffeeCupController(ICoffeeCupRepository coffeeCup, ICafeStationAspNetUserRepository cafeStationAspNetUserRepository, IHttpContextAccessor httpContextAccessor)
    {
        this.coffeeCup = coffeeCup;
        //this.currentCafeId = new Guid("B7C7D162-8871-4930-D55B-08DD08101897");
        this.cafeStationAspNetUserRepository = cafeStationAspNetUserRepository;
        this.httpContextAccessor = httpContextAccessor;
    }
    public IActionResult Index()
    {

        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();

        var resultOfCoffee = coffeeCup.GetAllBasedOnCafe(cafeId);
        if (resultOfCoffee.IsFailed)
            return NotFound();
        return View(resultOfCoffee.Result);
    }

    public IActionResult Add()
    {
        return View();
    }


    public IActionResult Edit(Guid Id)
    {
        var coffee = coffeeCup.Find(Id);
        return View("Add", coffee.Result);
    }
    [HttpPost]
    public IActionResult Edit([FromForm] AddCoffeeCupRequestModel request)
    {
        coffeeCup.Update(request);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Add([FromForm] AddCoffeeCupRequestModel request)
    {

        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();

        coffeeCup.Add(request.Name, cafeId, request.Price, request.Description, string.Empty);
        return View();
    }
}
