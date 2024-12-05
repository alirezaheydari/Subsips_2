namespace Repository.DataModel;

public class CoffeeCupOrder
{
    public Guid CoffeeId { get; set; }
    public Guid OrdersId { get; set; }

    public Order Order { get; set; } = null;
    public CoffeeCup Coffee { get; set; } = null;
}
