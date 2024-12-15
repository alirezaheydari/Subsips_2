using Repository.DataModel;
using Repository.Helper;
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
}
