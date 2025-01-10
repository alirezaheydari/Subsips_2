using System.ComponentModel.DataAnnotations;

namespace SawScan.DataAccess.Business;

public class BusinessCooperation
{
    [Key]
    public Guid Id { get; set; }
    [StringLength(64)]
    public required string Name { get; set; }
    [StringLength(64)]
    public required string FullName { get; set; }
    [StringLength(20)]
    public required string PhoneNumber { get; set; }
    public decimal Credit { get; set; }
    [StringLength(256)]
    public string? Description { get; set; }
    public List<Branche> Branches { get; set; } = [];
}
