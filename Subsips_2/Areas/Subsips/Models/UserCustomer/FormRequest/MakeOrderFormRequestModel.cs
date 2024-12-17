namespace Subsips_2.Areas.Subsips.Models.UserCustomer.FormRequest;

public class MakeOrderFormRequestModel
{
    public Guid CoffeeId { get; set; }
    public Guid OrderId { get; set; }
    public Guid CafeId { get; set; }
    public string? Description { get; set; }
}
