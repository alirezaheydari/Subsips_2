using Repository.Helper;

namespace Subsips_2.BusinessLogic.Cafe;

public interface ICafeStationAspNetUserRepository
{
    public Task<ReturnResult<bool>> Add(Guid cafeId, string userId, bool isActive, bool isOwner);
    public Guid FindCafeId(string userId);
}
