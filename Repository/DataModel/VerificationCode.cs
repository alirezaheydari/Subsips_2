using Repository.Helper.Validations;
using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class VerificationCode
{
    [Key]
    public Guid Id { get; set; }
    [StringLength(20)]
    public required string PhoneNumber { get; set; }
    [StringLength(10)]
    public required string Code { get; set; }
    public bool Verified { get; set; } = false;
    public DateTime CreateTime { get; set; }
    public DateTime ExpireTime { get; set; }

    public bool IsVerified(string code)
    {
        if (Code is null) return false;
        if (ExpireTime <= DateTime.Now) return false;
        if (Verified) return false;

        return Code.Equals(code);
    }

    public bool IsExpired()
    {
        return (ExpireTime <= DateTime.Now);
    }

    public static VerificationCode Create(string phoneNumber, string code)
    {


        var res = new VerificationCode
        {
            Id = Guid.NewGuid(),
            Code = code,
            PhoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(phoneNumber),
            CreateTime = DateTime.Now,
            ExpireTime = DateTime.Now.AddMinutes(2),
            Verified = false
        };

        return res;
    }
}
