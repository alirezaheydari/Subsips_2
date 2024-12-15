namespace Subsips_2.Areas.Subsips.Models.Cafe;

public class ConfirmedOrderModelView
{
    public Guid coffeeId { get; set; }
    public Guid orderId { get; set; }
    public Guid cafeId { get; set; }
    public required string CoffeeName { get; set; }
    public required string CustomerFullName { get; set; }
    public required string CustomerPhoneNumber { get; set; }
    public required string StationName { get; set; }
    public required string CafeName { get; set; }
    public decimal CoffeePrice { get; set; }

}
