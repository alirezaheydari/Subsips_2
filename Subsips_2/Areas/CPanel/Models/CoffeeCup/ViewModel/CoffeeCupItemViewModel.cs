using Repository.DataModel;

namespace Subsips_2.Areas.CPanel.Models.CoffeeCup.ViewModel;

public class CoffeeCupItemViewModel
{
    public Guid CoffeeId { get; set; }
    public string CoffeeName { get; set; }
    public decimal Price { get; set; }
}
