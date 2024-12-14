using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.Order.Filter;
using Subsips_2.Areas.CPanel.Models.Order.ViewModel;
using Subsips_2.Areas.Subsips.Models.UserCustomer;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.Order;

public class OrderRepository : IOrderRepository
{
    private readonly Subsips_2Context context;
    public OrderRepository(Subsips_2Context context)
    {
        this.context = context;
    }


    public async Task<ReturnResult<Repository.DataModel.Order>> MakeNewOrder(Guid orderId, string description, Guid cafeId, Guid coffeeId, Guid customerId)
    {
        context.CoffeeCupOrders.Add(new Repository.DataModel.CoffeeCupOrder
        {
            OrderId = orderId,
            CoffeeId = coffeeId,
            Count = 1
        });

        var newOrder = Repository.DataModel.Order.Create(orderId, description, cafeId,  customerId);

        context.Orders.Add(newOrder);

        await context.SaveChangesAsync();

        return ResultFactory.GetGoodResult(newOrder);
    }

    public ReturnResult<Repository.DataModel.Order> Find(Guid orderId)
    {
        var currentOrder = context.Orders.Find(orderId);

        if (currentOrder is null)
            return ResultFactory.GetBadResult<Repository.DataModel.Order>(new string[]
            {
                "Not Found"
            });

        return ResultFactory.GetGoodResult(currentOrder);
    }
    public ReturnResult<ShowStatusOrderViewModel> GetShowStatusOrderModelView(Guid orderId)
    {
        var res = context.Orders.Where(x => x.Id == orderId).Select(x => new ShowStatusOrderViewModel
        {
            CafePhoneNumber = x.Cafe.PhoneNumber ?? string.Empty,
            CoffeeName = x.CoffeeCupOrders.FirstOrDefault().Coffee.Name ?? string.Empty,
            StationCafeName = x.Cafe.Station.Name ?? string.Empty,
            CoffeePrice = x.CoffeeCupOrders.FirstOrDefault().Coffee.Price,
            CreateOrderTime = x.CreateDate,
            OrderId = orderId,
            Status = (OrderStatus)x.Status,
        })?.FirstOrDefault();


        return ResultFactory.GetGoodResult(res);
    }


    public ReturnResult<List<OrdersModelView>> GetOrdersModelView(OrderFilter filter)
    {

        var phoneNumberHasValue = false;
        var statusHasValue = false;
        var isJustToday = false;
        
        if (filter is not null)
        {
            phoneNumberHasValue = !filter.PhoneNumber.IsNullOrEmpty();
            statusHasValue = filter.Status != null;
            isJustToday = filter.TodayOrder;
        }
        var todayDate = DateTime.Now.Date;

        var res = context.Orders
            .Where(o => statusHasValue ? o.Status == (byte)filter.Status : true)
            .Where(o => isJustToday ? o.CreateDate.Date == DateTime.Now.Date : true)
            .Where(o => phoneNumberHasValue ? o.Customer.PhoneNumber.Contains(filter.PhoneNumber) : true)
            .Select(o => new OrdersModelView
        {
            CafePhoneNumber = o.Cafe.PhoneNumber,
            CoffeeName = o.CoffeeCupOrders.FirstOrDefault().Coffee.Name ?? string.Empty,
            CustomerFullName = o.Customer.FullName ?? string.Empty,
            CustomerPhoneNumber = o.Customer.PhoneNumber ?? string.Empty,
            CreateOrderDate = o.CreateDate,
            OrderId = o.Id,
            OrderStatus = (OrderStatus)o.Status
        }).ToList();

        return ResultFactory.GetGoodResult(res);
    }

    public async Task<bool> Confirm(Guid orderId)
    {
        var order = context.Orders.Find(orderId);
        order.Status = (byte)OrderStatus.Confirmed;
        await context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> Ready(Guid orderId)
    {
        var order = context.Orders.Find(orderId);
        order.Status = (byte)OrderStatus.Ready;
        await context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> Reject(Guid orderId)
    {
        var order = context.Orders.Find(orderId);
        order.Status = (byte)OrderStatus.Rejected;
        await context.SaveChangesAsync();
        return true;
    }
}
