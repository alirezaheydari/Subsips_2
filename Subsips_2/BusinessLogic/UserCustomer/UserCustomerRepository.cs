using Repository.Helper;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.UserCustomer;

public class UserCustomerRepository : IUserCustomerRepository
{
    private readonly Subsips_2Context context;

    public UserCustomerRepository(Subsips_2Context _context)
    {
        context = _context;
    }

    public async Task<ReturnResult<Repository.DataModel.UserCustomer>> Add(string phoneNumber, string fullname)
    {

        var res = Repository.DataModel.UserCustomer.Create(phoneNumber, fullname);


        context.UserCustomers.Add(res);


        await context.SaveChangesAsync();


        return ResultFactory.GetGoodResult(res);
    }

}
