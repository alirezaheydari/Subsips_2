using Repository.DataModel;

namespace Subsips_2.Areas.CPanel.Models.Admin.FormRequest;

public class AddEditStationFormRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public SubwayLines Line { get; set; }
    public bool IsActive { get; set; }
}
