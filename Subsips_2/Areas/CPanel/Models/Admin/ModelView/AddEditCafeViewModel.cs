namespace Subsips_2.Areas.CPanel.Models.Admin.ModelView;

public class AddEditCafeViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string? Description { get; set; }
    public Guid StationId { get; set; }
    public List<AddEditStationsCafeItem> Stations { get; set; }
}

public class AddEditStationsCafeItem
{
    public Guid Id { get; set; }
    public required string DisplayName { get; set; }
    public bool IsSelected { get; set; } = false;
}