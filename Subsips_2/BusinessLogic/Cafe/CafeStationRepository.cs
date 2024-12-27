using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.Admin.ModelView;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.Cafe;

public class CafeStationRepository : ICafeStationRepository
{
    private readonly Subsips_2Context context;

    public CafeStationRepository(Subsips_2Context context)
    {
        this.context = context;
    }


    public ReturnResult<CafeStation> Find(Guid cafeId)
    {
        var cafe = context.CafeStations.Find(cafeId);
        if (cafe is null)
            ResultFactory.GetBadResult(new string[]
            {
                "Model is not valid"
            });
        return ResultFactory.GetGoodResult(cafe);
    }

    public ReturnResult<List<CafeStationInfoViewModel>> GetCafeStationInfoViewModel()
    {
        var res = context.CafeStations.Select(x => new CafeStationInfoViewModel
        {
            CafeId = x.Id,
            CafeName = x.Name,
            StationName = x.Station.Name,
        }).ToList();


        return ResultFactory.GetGoodResult(res);
    }
}
