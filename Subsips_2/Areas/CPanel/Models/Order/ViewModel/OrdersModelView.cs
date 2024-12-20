using Repository.DataModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Subsips_2.Areas.CPanel.Models.Order.ViewModel;

public class OrdersModelView
{
    public List<OrderItemsViewModel>? Items { get; set; }
    public Filter.OrderFilter? Filter { get; set; }
}
public class OrderItemsViewModel
{
    public Guid OrderId { get; set; }
    public DateTime CreateOrderDate { get; set; }
    public required string CoffeeName { get; set; }
    public required string CustomerFullName { get; set; }
    public required string CustomerPhoneNumber { get; set; }
    public required string CafePhoneNumber { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string? Description { get; set; }
    public string GetRowClassName() 
    {
        switch (OrderStatus)
        {
            case OrderStatus.OnProcessed:
                return "table-danger";
            case OrderStatus.Ordered:
            case OrderStatus.Confirmed:
                return "table-warning";
            case OrderStatus.Rejected:
            case OrderStatus.Canceled:
                return "table-secondary";
            case OrderStatus.Ready:
            case OrderStatus.Completed:
                return "table-info";
        }


        return string.Empty;
    }

    public string GetStatusDisplayName
    {
        get
        {
            return OrderStatus.GetType()
                        .GetMember(OrderStatus.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()
                        .GetName();
        }
    }


}
