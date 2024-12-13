using Repository.Helper;
using Repository.Helper.Validations;
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
        phoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(phoneNumber);

        var existedUser = context.UserCustomers.FirstOrDefault(x => x.PhoneNumber == phoneNumber);

        if (existedUser is not null)
        {
            return ResultFactory.GetGoodResult(existedUser);
        }


        var res = Repository.DataModel.UserCustomer.Create(phoneNumber, fullname);


        context.UserCustomers.Add(res);


        await context.SaveChangesAsync();


        return ResultFactory.GetGoodResult(res);
    }

    public ReturnResult<Repository.DataModel.UserCustomer> Find(Guid customerId)
    {
        var currentCustomer = context.UserCustomers.Find(customerId);

        if (currentCustomer is null)
            ResultFactory.GetBadResult<Repository.DataModel.UserCustomer>(new string[]
            {
                "Not Found"
            });


        return ResultFactory.GetGoodResult(currentCustomer);
    }

}
