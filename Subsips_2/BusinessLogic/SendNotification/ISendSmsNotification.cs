using Repository.Helper;

namespace Subsips_2.BusinessLogic.SendNotification;

public interface ISendSmsNotification
{
    public ReturnResult<bool> SendVerificationCode(string phoneNumber, string code);
    public ReturnResult<bool> SendOrderToCafe(string phoneNumber, string coffeeName);
}
