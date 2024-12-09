using Repository.Helper;

namespace Repository.DataModel;

public class CustomerPhoneRegisterAuthentication
{
    public Guid Id { get; set; }
    public Guid UserCustomerId { get; set; }
    public Guid CafeId { get; set; }
    public DateTime ExpireDate { get; set; }


    public static ReturnResult<CustomerPhoneRegisterAuthentication> Create(Guid cafeId, Guid userCustomerId)
    {
        var res = new CustomerPhoneRegisterAuthentication()
        {
            CafeId = cafeId,
            UserCustomerId = userCustomerId,
            ExpireDate = DateTime.UtcNow.AddDays(8)
        };
        
        return ResultFactory.GetGoodResult(res);
    }
}
