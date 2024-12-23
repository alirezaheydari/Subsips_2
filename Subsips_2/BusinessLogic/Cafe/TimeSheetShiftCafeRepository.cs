using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.CafeStation.ModelView;
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

    public ReturnResult<List<TimeSheetItem>> GetTimeSheetsOfCafe(Guid cafeId)
    {
        var res = context.TimeSheetsShifts.Where(x => x.CafeId == cafeId).ToList();

        return ResultFactory.GetGoodResult(res.Select(x => new TimeSheetItem
        {
            DayOfWeek = x.DayOfWeek,
            EndTime = x.EndTime,
            PhoneNumber = x.PhoneNumber,
            IsActive = x.IsActive,
            StartTime = x.StartTime,
            Id = x.Id
        }).ToList());
    }


    public async Task<ReturnResult<bool>> Remove(Guid Id, Guid cafeId)
    {
        var res = context.TimeSheetsShifts.First(x => x.Id == Id && x.CafeId == cafeId);
        if (res is null)
            return ResultFactory.GetBadResult(new string[]
            {
                "model is not valid"
            });
        context.TimeSheetsShifts.Remove(res);
        await context.SaveChangesAsync();

        return ResultFactory.GetGoodResult();
    }

    public async Task<ReturnResult<bool>> ChangeIsActiveState(Guid Id, Guid cafeId)
    {
        var res = context.TimeSheetsShifts.First(x => x.Id == Id && x.CafeId == cafeId);
        if (res is null)
            return ResultFactory.GetBadResult(new string[]
            {
                "model is not valid"
            });

        res.IsActive = !res.IsActive;

        await context.SaveChangesAsync();

        return ResultFactory.GetGoodResult();
    }
}
