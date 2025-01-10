using System.ComponentModel.DataAnnotations;

namespace SawScan.DataAccess.Business;

public class BusinessCategory
{
    [Key]
    public Guid Id { get; set; }
    [StringLength(64)]
    public required string Name { get; set; }

    public List<Branche> Branches { get; set; } = [];
}
