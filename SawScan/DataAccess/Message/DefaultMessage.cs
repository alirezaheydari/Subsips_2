using SawScan.DataAccess.Business;
using System.ComponentModel.DataAnnotations;

namespace SawScan.DataAccess.Message;

public class DefaultMessage
{
    [Key]
    public Guid Id { get; set; }
    public Guid BrancheId { get; set; }
    [StringLength(256)]
    public string? MessageContext { get; set; }
    public bool IsActive { get; set; }
    public Branche Branche { get; set; }
}
