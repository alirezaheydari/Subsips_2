using System.ComponentModel.DataAnnotations;

namespace SawScan.DataAccess.Message;

public class SentSMSMessage
{
    public Guid Id { get; set; }
    [StringLength(256)]
    public string? MessageText { get; set; }
    [StringLength(20)]
    public required string PhoneNumber { get; set; }
}
