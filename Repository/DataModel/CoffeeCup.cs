using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class CoffeeCup
{
    public Guid Id { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    [MaxLength(200)]
    public string ImageUrl { get; set; } = string.Empty;

    public Guid CafeId { get; set; }

    public CafeStation Cafe { get; set; }
    public List<Order> Orders { get; set; } = [];
    //public List<CoffeeCupOrder> CoffeeCupOrders { get; set; } = [];
}
