using Repository.Helper;
using Subsips_2.Data;
using System.Net.WebSockets;

namespace Subsips_2.BusinessLogic.Order;

public class OrderRepository : IOrderRepository
{
    private readonly Subsips_2Context context;
    public OrderRepository(Subsips_2Context context)
    {
        this.context = context;
    }


    public async Task<ReturnResult<bool>> MakeNewOrder(Guid orderId, string description, Guid cafeId, Guid coffeeId, Guid customerId)
    {
        var newOrder = Repository.DataModel.Order.Create(orderId, description, cafeId, coffeeId, customerId);



        context.Orders.Add(newOrder);

        await context.SaveChangesAsync();


        return ResultFactory.GetGoodResult();
    }
}
