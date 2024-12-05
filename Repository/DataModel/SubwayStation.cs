using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class SubwayStation
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public byte Line { get; set; }
    [MaxLength(150)]
    public string Description { get; set; } = string.Empty;
    public Guid CafeId { get; set; }


    public CafeStation Cafe { get; set; }
}
