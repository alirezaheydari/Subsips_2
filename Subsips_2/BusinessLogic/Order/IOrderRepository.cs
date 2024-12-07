using Repository.Helper;

namespace Subsips_2.BusinessLogic.Order;

public interface IOrderRepository
{
    public Task<ReturnResult<bool>> MakeNewOrder(Guid orderId, string description, Guid cafeId, Guid coffeeId, Guid customerId);
}
