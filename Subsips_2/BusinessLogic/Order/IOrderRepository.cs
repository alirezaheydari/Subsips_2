﻿using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.Order.Filter;
using Subsips_2.Areas.CPanel.Models.Order.ViewModel;
using Subsips_2.Areas.Subsips.Models.UserCustomer;

namespace Subsips_2.BusinessLogic.Order;

public interface IOrderRepository
{
    public Task<ReturnResult<Repository.DataModel.Order>> MakeNewOrder(Guid orderId, string description, Guid cafeId, Guid coffeeId, Guid customerId, EstimateDelivery estimate);
    public ReturnResult<Repository.DataModel.Order> Find(Guid orderId);
    public ReturnResult<List<OrderItemsViewModel>> GetOrdersModelView(OrderFilter? filter);
    public ReturnResult<ShowStatusOrderViewModel> GetShowStatusOrderModelView(Guid orderId);
    public ReturnResult<List<UserOrderItem>> GetAllOrdersOfCustomer(Guid customerId);
    public Task<bool> Confirm(Guid orderId);
    public Task<bool> Ready(Guid orderId);
    public Task<bool> Reject(Guid orderId);
}
