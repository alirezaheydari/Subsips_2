using Repository.DataModel;

namespace Subsips_2.Areas.CPanel.Models.Admin.ModelView;

public class StationsModeView
{
    public List<StationItemModeView> Items { get; set; } = new List<StationItemModeView>();
}

public class StationItemModeView
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }

    public required string Name { get; set; }
    public SubwayLines Line { get; set; }
    public string? Description { get; set; }
}
