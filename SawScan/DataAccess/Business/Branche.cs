using SawScan.DataAccess.Customer;
using SawScan.DataAccess.Message;
using System.ComponentModel.DataAnnotations;

namespace SawScan.DataAccess.Business;

public class Branche
{
    [Key]
    public Guid Id { get; set; }
    public Guid BusinessId { get; set; }
    public Guid CategoryId { get; set; }
    [StringLength(64)]
    public required string Name { get; set; }
    [StringLength(20)]
    public required string PhoneNumber { get; set; }
    public decimal Credit { get; set; }

    [StringLength(128)]
    public string? Address { get; set; }

    [StringLength(128)]
    public string? LogoUrl { get; set; }
    [StringLength(128)]
    public string? InstagramLink { get; set; }
    [StringLength(128)]
    public string? TelegramLink { get; set; }
    [StringLength(128)]
    public string? XLink { get; set; }
    [StringLength(256)]
    public string? Description { get; set; }

    public BusinessCooperation Business { get; set; }
    public BusinessCategory Category { get; set; }
    public DefaultMessage DefaultMessage { get; set; }
    public List<ExclusiveCustomer> Customers { get; set; } = [];
}
