using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.CafeStation.ModelView;

namespace Subsips_2.BusinessLogic.ExclusiveCustomer;

public interface IExclusiveCafeCustomerRepository
{
    public Task<ReturnResult<bool>> Add(Guid cafeId, string phoneNumebr, string fullName);
    public ReturnResult<List<ExclusiveUsersItemModelView>> GetExclusive(Guid cafeId);
}
