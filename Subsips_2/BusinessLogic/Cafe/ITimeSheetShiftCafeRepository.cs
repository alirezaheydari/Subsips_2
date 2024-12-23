using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.CafeStation.ModelView;

namespace Subsips_2.BusinessLogic.Cafe;

public interface ITimeSheetShiftCafeRepository
{
    public Task<ReturnResult<TimeSheetShiftCafe>> Add(Guid cafeId, DateTime startTime, DateTime endTime, bool isActive, string phoneNumber, DayOfWeek dayOfWeek);
    public ReturnResult<List<TimeSheetItem>> GetTimeSheetsOfCafe(Guid cafeId);
    public Task<ReturnResult<bool>> Remove(Guid Id, Guid cafeId);
    public Task<ReturnResult<bool>> ChangeIsActiveState(Guid Id, Guid cafeId);
}
