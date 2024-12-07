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

    public static Order Create(Guid id, string description, Guid cafeId, Guid coffeeId, Guid customerId)
    {
        var resutl = new Order();

        resutl.Id = id;
        resutl.Description = description ?? string.Empty;
        resutl.CreateDate = DateTime.Now;
        resutl.CoffeeIds = new List<Guid> { coffeeId };
        resutl.CafeId = cafeId;
        resutl.CustomerId = customerId;
        resutl.Status = (byte)OrderStatus.OnProcessed;


        return resutl;
        
    }

    public CafeStation Cafe { get; set; }
    public List<CoffeeCup> Coffee { get; set; } = [];
    //public List<CoffeeCupOrder> CoffeeCupOrders { get; set; } = [];
    public UserCustomer Customer { get; set; }
}


public enum OrderStatus
{
    OnProcessed,
    Ordered,
    Confirmed,
    Rejected,
    Canceled,
    Ready,
    Completed
}