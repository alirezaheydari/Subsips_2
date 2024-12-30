using Microsoft.EntityFrameworkCore;
using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.CafeConfig;

public class CafeStationConfigRepository : ICafeStationConfigRepository
{
    private readonly Subsips_2Context context;

    public CafeStationConfigRepository(Subsips_2Context context)
    {
        this.context = context;
    }

    public async Task<ReturnResult<bool>> Add(Guid cafeId, string defaultSmsMsg, bool isSendSms = false, bool IsActive = false)
    {
        var model = CafeStationConfig.Create(cafeId, defaultSmsMsg, isSendSms, IsActive);
        context.CafeStationConfigs.Add(model);

        await context.SaveChangesAsync();

        return ResultFactory.GetGoodResult();
    }

    public ReturnResult<List<CafeStationConfig>> GetAll()
    {
        var res = context.CafeStationConfigs.ToList();


        return ResultFactory.GetGoodResult(res);
    }


    public ReturnResult<CafeStationConfig> Find(Guid id)
    {
        var res = context.CafeStationConfigs.Find(id);


        return ResultFactory.GetGoodResult(res);
    }


    public ReturnResult<CafeStationConfig> FindActiveByCafeId(Guid cafeId)
    {
        var res = context.CafeStationConfigs.Where(x => x.IsActive && x.CafeId == cafeId).FirstOrDefault();

        if (res is null)
            return ResultFactory.GetBadResult<CafeStationConfig>(new string[]
            {
                "Model is not valid"
            });

        return ResultFactory.GetGoodResult(res);
    }
}
