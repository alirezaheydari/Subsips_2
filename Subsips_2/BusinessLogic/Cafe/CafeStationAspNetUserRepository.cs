using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.Cafe;

public class CafeStationAspNetUserRepository: ICafeStationAspNetUserRepository
{
    private readonly Subsips_2Context context;

    public CafeStationAspNetUserRepository(Subsips_2Context context)
    {
        this.context = context;
    }


    public async Task<ReturnResult<bool>> Add(Guid cafeId, string userId, bool isActive, bool isOwner)
    {
        var resModel = CafeStationAspNetUser.Create(cafeId, userId, isActive, isOwner);


        var savedModel = context.CafeStationAspNetUsers.Add(resModel);



        await context.SaveChangesAsync();


        return ResultFactory.GetGoodResult();
    }
}
