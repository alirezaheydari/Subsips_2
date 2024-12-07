namespace Subsips_2.Areas.Subsips.Models.UserCustomer;

public class PhoneNumberRegisterFormRequestModel
{
    public Guid OrderId { get; set; }
    public Guid CoffeeId { get; set; }
    public required string PhoneNumber { get; set; }
}
