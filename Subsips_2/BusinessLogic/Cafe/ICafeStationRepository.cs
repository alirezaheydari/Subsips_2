using Repository.DataModel;
using Repository.Helper;

namespace Subsips_2.BusinessLogic.Cafe;

public interface ICafeStationRepository
{
    public ReturnResult<CafeStation> Find(Guid cafeId);
}
