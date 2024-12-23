namespace Subsips_2.Areas.CPanel.Models.CafeStation;

public class TimeSheetShiftAddRequest
{
    public Guid Id { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public bool IsActive { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public required string PhoneNumber { get; set; }
}
