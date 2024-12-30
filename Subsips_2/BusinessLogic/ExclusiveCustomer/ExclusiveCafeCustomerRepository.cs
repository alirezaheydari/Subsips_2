using Repository.DataModel;
using Repository.Helper;
using Repository.Helper.Validations;
using Subsips_2.Areas.CPanel.Models.CafeStation.ModelView;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.ExclusiveCustomer;

public class ExclusiveCafeCustomerRepository : IExclusiveCafeCustomerRepository
{
    private readonly Subsips_2Context context;

    public ExclusiveCafeCustomerRepository(Subsips_2Context context)
    {
        this.context = context;
    }

    public async Task<ReturnResult<bool>> Add(Guid cafeId, string phoneNumebr, string fullName)
    {
        phoneNumebr = PhoneNumberValidation.GetPhoneNumberWithoutZero(phoneNumebr);
        var isExists = context.ExclusiveCafeCustomers.Any(x => x.CustomerPhoneNumber == phoneNumebr && x.CafeId == cafeId);

        if (isExists)
            return ResultFactory.GetGoodResult();

        var model = ExclusiveCafeCustomer.Create(cafeId, phoneNumebr, fullName);

        context.ExclusiveCafeCustomers.Add(model);

        await context.SaveChangesAsync();

        return ResultFactory.GetGoodResult();
    }

    public ReturnResult<List<ExclusiveUsersItemModelView>> GetExclusive(Guid cafeId)
    {
        var res = context.ExclusiveCafeCustomers.Where(x => x.CafeId ==  cafeId).Select(x => new ExclusiveUsersItemModelView
        {
            PhoneNumber = x.CustomerPhoneNumber,
            Id = x.Id,
            FullName = x.CustomerFullName
        }).ToList();

        return ResultFactory.GetGoodResult(res);
    }
    
}
