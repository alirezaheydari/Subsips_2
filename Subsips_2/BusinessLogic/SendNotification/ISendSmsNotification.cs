using Repository.DataModel;
using Repository.Helper;

namespace Subsips_2.BusinessLogic.SendNotification;

public interface ISendSmsNotification
{
    public ReturnResult<bool> SendVerificationCode(string phoneNumber, string code);
    public ReturnResult<bool> SendOrderToCafe(string phoneNumber, string coffeeName, string fullName, string userPhoneNumber, EstimateDelivery estimate);
    public ReturnResult<bool> SendDefaultMsgForCafe(string phoneNumber, string msgContext);
}
