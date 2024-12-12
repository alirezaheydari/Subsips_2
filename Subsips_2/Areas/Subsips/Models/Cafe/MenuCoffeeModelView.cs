namespace Subsips_2.Areas.Subsips.Models.Cafe;

public class MenuCoffeeModelView
{
    public Guid CoffeeId { get; set; }
    public Guid CafeId { get; set; }
    public string CoffeeName { get; set; } = string.Empty;
    public decimal PriceOfCoffee { get; set; }
    public string CoffeeDesciption { get; set; } = string.Empty;

}
