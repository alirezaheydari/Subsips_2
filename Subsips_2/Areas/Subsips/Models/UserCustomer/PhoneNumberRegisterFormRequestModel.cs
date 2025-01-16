using Microsoft.IdentityModel.Tokens;
using Repository.DataModel;
using Repository.Helper.Validations;
using Subsips_2.Areas.Subsips.Models.UserCustomer.FormRequest;

namespace Subsips_2.Areas.Subsips.Models.UserCustomer;

public class PhoneNumberRegisterFormRequestModel : UserRegisterFormRequestModel
{
    public Guid OrderId { get; set; }
    public Guid CoffeeId { get; set; }
    public Guid CafeId { get; set; }
    public required string FullName { get; set; }

    public string Estimate { get; set; }
    public EstimateDelivery EstimateDeliver
    {
        get
        {
            if (Estimate == "10")
                return EstimateDelivery.TenMin;
            return EstimateDelivery.FiveMin;
        }
    }

    public string Description { get; set; }
    public bool IsValid()
    {
        if (OrderId == Guid.Empty || CoffeeId == Guid.Empty)
            return false;

        if (FullName.IsNullOrEmpty())
            return false;

        PhoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(PhoneNumber);

        return PhoneNumberValidation.IsValid(PhoneNumber);
    }
}


public class UserRegisterFormRequestModel
{
    public required string PhoneNumber { get; set; }
    public required string OtpCode { get; set; }


    public bool IsPhoneNumberValid() {

        PhoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(PhoneNumber);
        return PhoneNumberValidation.IsValid(PhoneNumber);
    }
}

    public class SendOtpFormRequestModel
{
    public required string PhoneNumber { get; set; }
    public bool IsPhoneNumberValid()
    {
        PhoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(PhoneNumber);
        return PhoneNumberValidation.IsValid(PhoneNumber);
    }
}