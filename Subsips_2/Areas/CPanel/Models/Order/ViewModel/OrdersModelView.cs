using Repository.DataModel;

namespace Subsips_2.Areas.CPanel.Models.Order.ViewModel;

public class OrdersModelView
{
    public Guid OrderId { get; set; }
    public DateTime CreateOrderDate { get; set; }
    public required string CoffeeName { get; set; }
    public required string CustomerFullName { get; set; }
    public required string CustomerPhoneNumber { get; set; }
    public required string CafePhoneNumber { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
