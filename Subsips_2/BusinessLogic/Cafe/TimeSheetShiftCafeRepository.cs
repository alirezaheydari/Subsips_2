using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.Cafe;

public class TimeSheetShiftCafeRepository : ITimeSheetShiftCafeRepository
{
    private readonly Subsips_2Context context;

    public TimeSheetShiftCafeRepository(Subsips_2Context context)
    {
        this.context = context;
    }

    public async Task<ReturnResult<TimeSheetShiftCafe>> Add(Guid cafeId, DateTime startTime, DateTime endTime, bool isActive, string phoneNumber, DayOfWeek dayOfWeek)
    {

        var model = TimeSheetShiftCafe.Create(cafeId, startTime, endTime, isActive, phoneNumber, dayOfWeek);

        if (model.IsFailed)
            return model;

        context.TimeSheetsShifts.Add(model.Result);

        await context.SaveChangesAsync();




        return ResultFactory.GetGoodResult(model.Result);
    }

}
