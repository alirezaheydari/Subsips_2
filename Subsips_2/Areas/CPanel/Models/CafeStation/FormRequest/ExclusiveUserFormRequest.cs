namespace Subsips_2.Areas.CPanel.Models.CafeStation.FormRequest;

public class ExclusiveUserFormRequest
{
    public required string PhoneNumber { get; set; }
    public string? FullName { get; set; }
    public Guid CafeId { get; set; }
}
