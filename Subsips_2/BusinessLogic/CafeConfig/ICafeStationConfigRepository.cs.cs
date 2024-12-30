using Repository.DataModel;
using Repository.Helper;

namespace Subsips_2.BusinessLogic.CafeConfig;

public interface ICafeStationConfigRepository
{
    public Task<ReturnResult<bool>> Add(Guid cafeId, string defaultSmsMsg, bool isSendSms = false, bool IsActive = false);
    public ReturnResult<List<CafeStationConfig>> GetAll();
    public ReturnResult<CafeStationConfig> FindActiveByCafeId(Guid cafeId);
}
