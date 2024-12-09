using Repository.Helper.Validations;

namespace Subsips_2.Areas.Subsips.Models.UserCustomer;

public class PhoneNumberRegisterFormRequestModel
{
    public Guid OrderId { get; set; }
    public Guid CoffeeId { get; set; }
    public required string PhoneNumber { get; set; }

    public bool IsValid()
    {
        if (OrderId == Guid.Empty || CoffeeId == Guid.Empty)
            return false;
        return PhoneNumberValidation.IsValid(PhoneNumber);
    }
}
