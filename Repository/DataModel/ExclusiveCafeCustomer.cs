using Repository.Helper.Validations;
using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class ExclusiveCafeCustomer
{
    public Guid Id { get; set; }
    public Guid CafeId { get; set; }

    [StringLength(90)]
    public string? CustomerFullName { get; set; }

    [StringLength(20)]
    public required string CustomerPhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public CafeStation Cafe { get; set; }


    public static ExclusiveCafeCustomer Create(Guid cafeId, string phoneNumebr, string fullName)
    {
        var res = new ExclusiveCafeCustomer
        {
            CustomerPhoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(phoneNumebr),
            CafeId = cafeId,
            CreateDate = DateTime.Now,
            IsActive = true,
            CustomerFullName = fullName
        };

        return res;
    }
}
