using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.DataModel;

public class CoffeeCupOrder
{
    public Guid CoffeeId { get; set; }
    public Guid OrderId { get; set; }
    public int Count { get; set; } = 1;

    [ForeignKey("OrderId")]
    public Order Order { get; set; } = null;
    public CoffeeCup Coffee { get; set; } = null;
}
