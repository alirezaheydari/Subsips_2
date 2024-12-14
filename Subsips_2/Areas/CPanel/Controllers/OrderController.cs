using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.DataModel;
using Subsips_2.Areas.CPanel.Models.Order.Filter;
using Subsips_2.BusinessLogic.Order;

namespace Subsips_2.Areas.CPanel.Controllers;

[Area("CPanel")]
public class OrderController : Controller
{
    private readonly IOrderRepository orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public IActionResult Index()
    {
        var resultOrders = orderRepository.GetOrdersModelView(null);
        if (resultOrders is null || resultOrders.IsFailed)
            return NotFound();


        return View(resultOrders.Result);
    }

    [HttpPost]
    public IActionResult Index(OrderFilter filter)
    {
        var resultOrders = orderRepository.GetOrdersModelView(filter);
        if (resultOrders is null || resultOrders.IsFailed)
            return NotFound();


        return View(resultOrders.Result);
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
