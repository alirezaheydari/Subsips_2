using Repository.DataModel;
using Repository.Helper;

namespace Subsips_2.BusinessLogic.Cafe;

public interface ITimeSheetShiftCafeRepository
{
    public Task<ReturnResult<TimeSheetShiftCafe>> Add(Guid cafeId, DateTime startTime, DateTime endTime, bool isActive, string phoneNumber, DayOfWeek dayOfWeek);
}
