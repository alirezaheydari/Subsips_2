using System.ComponentModel.DataAnnotations;


namespace Repository.DataModel;

public class CafeStation
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Description { get; set; }
    public Guid StationId { get; set; }

    public SubwayStation Station { get; set; }

    public List<Order> Orders { get; set; } = [];
    public List<CoffeeCup> Coffee { get; set; } = [];
}
