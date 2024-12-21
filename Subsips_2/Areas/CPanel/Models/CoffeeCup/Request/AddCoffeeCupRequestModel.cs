namespace Subsips_2.Areas.CPanel.Models.CoffeeCup;

public class AddCoffeeCupRequestModel
{
    public Guid? CoffeeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
