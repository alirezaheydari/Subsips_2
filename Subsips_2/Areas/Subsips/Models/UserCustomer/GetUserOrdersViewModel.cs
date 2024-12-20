using Repository.DataModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Subsips_2.Areas.Subsips.Models.UserCustomer;

public class GetUserOrdersViewModel
{
    public List<UserOrderItem> Items { get; set; }
}

public class UserOrderItem
{
    public Guid orderId { get; set; }
    public string CoffeeName { get; set; }
    public string StationName { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreateOrderDate { get; set; }

    public string GetStatusDisplayName
    {
        get
        {
            return Status.GetType()
                        .GetMember(Status.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()
                        .GetName();
        }
    }

    public string GetCreateDateDisplay
    {
        get
        {
            var calendar = new PersianCalendar();
            var persianDate = new DateTime(calendar.GetYear(CreateOrderDate), calendar.GetMonth(CreateOrderDate), calendar.GetDayOfMonth(CreateOrderDate), CreateOrderDate.Hour, CreateOrderDate.Minute, CreateOrderDate.Second);
            var result = persianDate.ToString("yyyy/MM/dd - HH:mm");
            return result;
        }
    }
}
