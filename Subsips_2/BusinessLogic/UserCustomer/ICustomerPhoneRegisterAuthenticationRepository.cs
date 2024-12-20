using Repository.DataModel;
using Repository.Helper;

namespace Subsips_2.BusinessLogic.UserCustomer;

public interface ICustomerPhoneRegisterAuthenticationRepository
{
    public Task<ReturnResult<Guid>> Add(Guid cafeId, Guid userCustomerId);
    public Task<ReturnResult<bool>> ReloadTockenRegisterationAsync(Guid registerId);
    public ReturnResult<CustomerPhoneRegisterAuthentication> Get(Guid registerId);
}
