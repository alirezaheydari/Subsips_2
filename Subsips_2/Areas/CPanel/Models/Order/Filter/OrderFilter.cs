using Repository.DataModel;

namespace Subsips_2.Areas.CPanel.Models.Order.Filter;

public class OrderFilter
{
    public string? PhoneNumber { get; set; }
    public OrderStatus? Status { get; set; }
    public Guid CafeId { get; set; }
    public bool TodayOrder { get; set; } = false;
}
