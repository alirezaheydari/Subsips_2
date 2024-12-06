using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class Order
{
    public Guid Id { get; set; }
    public byte Status { get; set; }
    [MaxLength(150)]
    public string Description { get; set; } = string.Empty;
    public Guid CafeId { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public IEnumerable<Guid> CoffeeIds { get; set; } = [];

    public CafeStation Cafe { get; set; }
    public List<CoffeeCup> Coffee { get; set; } = [];
    //public List<CoffeeCupOrder> CoffeeCupOrders { get; set; } = [];
    public UserCustomer Customer { get; set; }
}
