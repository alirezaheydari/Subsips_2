using mpNuget;
using Repository.Helper;

namespace Subsips_2.BusinessLogic.SendNotification;

public class SendSmsNotification : ISendSmsNotification
{
    private string fromPhoneNumber = "50002710063488";

    private static string _melliPayamkPassword = "45089b4c-02ea-4ff1-8746-b4a3c029d08e";
    private static string _melliPayamkUsername = "09120655488";
    private static int _melliPayamkBodyIdForOTP = 274619;

    public ReturnResult<bool> SendVerificationCode(string phoneNumber, string code)
    {
        var restClient = new RestClient(_melliPayamkUsername, _melliPayamkPassword);
        var res = restClient.SendByBaseNumber(code, phoneNumber, _melliPayamkBodyIdForOTP);
        return ResultFactory.GetGoodResult();
    }

    public ReturnResult<bool> SendOrderToCafe(string phoneNumber, string coffeeName)
    {
        try
        {
            var restClient = new RestClient(_melliPayamkUsername, _melliPayamkPassword);
            var msgContext = $"یک سفارش  {coffeeName} ثبت شد";
            var res = restClient.Send(phoneNumber, fromPhoneNumber, msgContext, false);
            return ResultFactory.GetGoodResult();
        }
        catch (Exception ex)
        {
            return ResultFactory.GetBadResult(new string[] {
                "model is not valid SendOrderToCafe",
                ex.Message
            });
        }
    }
}
