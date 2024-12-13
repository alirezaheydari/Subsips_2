using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class CoffeeCup
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }

    public Guid CafeId { get; set; }


    public static CoffeeCup Create(string name, Guid cafeId, decimal price, string description, string imageUrl)
    {
        var model = new CoffeeCup
        {
            Name = name,
            CafeId = cafeId,
            Description = description ?? string.Empty,
            Price = price,
            ImageUrl = imageUrl ?? string.Empty
        };




        return model;
    }


    public CafeStation Cafe { get; set; }
    //public List<Order> Orders { get; set; } = [];
    public List<CoffeeCupOrder> CoffeeCupOrders { get; set; } = [];
}
