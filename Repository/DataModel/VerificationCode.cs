using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class VerificationCode
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(20)]
    public required string PhoneNumber { get; set; }
    [MaxLength(10)]
    public required string Code { get; set; }
    public bool Verified { get; set; } = false;
    public DateTime CreateTime { get; set; }
    public DateTime ExpireTime { get => CreateTime.AddMinutes(2); }
}
