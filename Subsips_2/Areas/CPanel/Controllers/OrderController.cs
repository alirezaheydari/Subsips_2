using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.DataModel;
using Subsips_2.Areas.CPanel.Models.Order.Filter;
using Subsips_2.Areas.CPanel.Models.Order.ViewModel;
using Subsips_2.BusinessLogic.Cafe;
using Subsips_2.BusinessLogic.Order;
using System.Security.Claims;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("CPanel")]
[Authorize]
public class OrderController : Controller
{
    private readonly IOrderRepository orderRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ICafeStationAspNetUserRepository cafeStationAspNetUserRepository;
    public OrderController(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor, ICafeStationAspNetUserRepository cafeStationAspNetUserRepository)
    {
        this.orderRepository = orderRepository;
        this.httpContextAccessor = httpContextAccessor;
        this.cafeStationAspNetUserRepository = cafeStationAspNetUserRepository;
    }

    public IActionResult Index(OrderFilter filter)
    {
        var username = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var cafeId = cafeStationAspNetUserRepository.FindCafeId(username);

        if (cafeId == Guid.Empty)
            return NotFound();

        filter.CafeId = cafeId;


        var resultOrders = orderRepository.GetOrdersModelView(filter);
        if (resultOrders is null || resultOrders.IsFailed)
            return NotFound();


        var res = new OrdersModelView
        {
            Filter = filter,
            Items = resultOrders.Result?.OrderByDescending(x => x.CreateOrderDate).ToList()
        };

        return View(res);
    }



    [HttpPost]
    public IActionResult Confirm([FromBody] Guid orderId)
    {
        orderRepository.Confirm(orderId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Ready([FromBody] Guid orderId)
    {
        orderRepository.Ready(orderId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Reject([FromBody] Guid orderId)
    {
        orderRepository.Reject(orderId);
        return RedirectToAction("Index");
    }
}
