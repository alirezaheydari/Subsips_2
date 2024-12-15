namespace Subsips_2.Areas.Subsips.Models.Cafe;

public class CoffeeConfirmationInfo
{
    public required string CoffeeName { get; set; }
    public required string CafeName { get; set; }
    public required string StationName { get; set; }
    public decimal Price { get; set; }
}
