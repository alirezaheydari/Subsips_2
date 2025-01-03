namespace Subsips_2.Areas.CPanel.Models.Admin.ModelView;

public class CafesViewModel
{
    public List<CafeItemViewModel> Items { get; set; }
}
public class CafeItemViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string StationName { get; set; }
    public string PhoneNumber { get; set; }
}
