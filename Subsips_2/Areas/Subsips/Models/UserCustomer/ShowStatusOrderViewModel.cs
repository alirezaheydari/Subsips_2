using Repository.DataModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Subsips_2.Areas.Subsips.Models.UserCustomer;

public class ShowStatusOrderViewModel
{
    public Guid OrderId { get; set; }
    public DateTime CreateOrderTime { get; set; }
    public required string CoffeeName { get; set; }
    public required string CafePhoneNumber { get; set; }
    public decimal CoffeePrice { get; set; }
    public required string StationCafeName { get; set; }
    public OrderStatus Status { get; set; }

    public string GetStatusDisplayName
    {
        get {
            return Status.ToString();
        }
    }


    public string GetImageStatusOrder()
    {
        switch (Status)
        {
            case OrderStatus.OnProcessed:
            case OrderStatus.Ordered:
            case OrderStatus.Confirmed:
            case OrderStatus.Ready:
                return "on_progress_order";
            case OrderStatus.Rejected:
            case OrderStatus.Canceled:
                return "reject_order";
            case OrderStatus.Completed:
                return "compeleted_order";
        }

        return string.Empty;
    }
}
