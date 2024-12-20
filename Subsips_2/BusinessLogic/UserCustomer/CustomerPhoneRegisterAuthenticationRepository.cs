using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.UserCustomer;

public class CustomerPhoneRegisterAuthenticationRepository : ICustomerPhoneRegisterAuthenticationRepository
{
    private readonly Subsips_2Context _context;
    public CustomerPhoneRegisterAuthenticationRepository(Subsips_2Context _context)
    {
        this._context = _context;
    }

    public async Task<ReturnResult<Guid>> Add(Guid cafeId, Guid userCustomerId)
    {
        var res = Repository.DataModel.CustomerPhoneRegisterAuthentication.Create(cafeId, userCustomerId);

        if (res.IsFailed)
            return ResultFactory.GetBadResult<Guid>(new List<string>
            {
                "Model is not Created"
            });

        _context.CustomerPhoneRegisterAuthentications.Add(res.Result);


        await _context.SaveChangesAsync();

        return ResultFactory.GetGoodResult(res.Result.Id);
    }

    public async Task<ReturnResult<bool>> ReloadTockenRegisterationAsync(Guid registerId)
    {
        var registerRecord = _context.CustomerPhoneRegisterAuthentications.Find(registerId);
        registerRecord.ReloadTockenRegisteration();
        await _context.SaveChangesAsync();
        return ResultFactory.GetGoodResult();
    }
    public ReturnResult<CustomerPhoneRegisterAuthentication> Get(Guid registerId)
    {
        var registerRecord = _context.CustomerPhoneRegisterAuthentications.Find(registerId);

        if (registerRecord is null || (!registerRecord.IsNotExpired()))
            return ResultFactory.GetBadResult<CustomerPhoneRegisterAuthentication>(new string[] {
                "this is Unregister."
            });


        return ResultFactory.GetGoodResult(registerRecord);
    }

}
