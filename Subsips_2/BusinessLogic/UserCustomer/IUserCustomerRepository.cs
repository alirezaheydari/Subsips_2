using Repository.Helper;

namespace Subsips_2.BusinessLogic.UserCustomer;

public interface IUserCustomerRepository
{
    public Task<ReturnResult<Repository.DataModel.UserCustomer>> Add(string phoneNumber, string fullname);
    public ReturnResult<Repository.DataModel.UserCustomer> Find(Guid customerId);
}
