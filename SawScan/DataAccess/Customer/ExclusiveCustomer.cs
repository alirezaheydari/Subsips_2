using SawScan.DataAccess.Business;
using System.ComponentModel.DataAnnotations;

namespace SawScan.DataAccess.Customer;

public class ExclusiveCustomer
{
    public Guid Id { get; set; }
    public Guid BrancheId { get; set; }
    [StringLength(64)]
    public required string FullName { get; set; }
    public Gender Gender { get; set; }
    public Branche Branche { get; set; }
}

public enum Gender
{
    Male,
    Female
}
