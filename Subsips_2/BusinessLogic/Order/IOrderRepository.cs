using Repository.Helper;
using Subsips_2.Areas.Subsips.Models.UserCustomer;

namespace Subsips_2.BusinessLogic.Order;

public interface IOrderRepository
{
    public Task<ReturnResult<Repository.DataModel.Order>> MakeNewOrder(Guid orderId, string description, Guid cafeId, Guid coffeeId, Guid customerId);
    public ReturnResult<Repository.DataModel.Order> Find(Guid orderId);

    public ReturnResult<ShowStatusOrderViewModel> GetShowStatusOrderModelView(Guid orderId);
}
