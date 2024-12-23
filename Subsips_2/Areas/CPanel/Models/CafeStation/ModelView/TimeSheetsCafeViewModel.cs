using System.ComponentModel.DataAnnotations;

namespace Subsips_2.Areas.CPanel.Models.CafeStation.ModelView;

public class TimeSheetsCafeViewModel
{
    public string CafeName { get; set; }
    public List<TimeSheetItem> Items { get; set; }
}

public class TimeSheetItem
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    [StringLength(20)]
    public required string PhoneNumber { get; set; }
}
