using mpNuget;
using Repository.DataModel;
using Repository.Helper;

namespace Subsips_2.BusinessLogic.SendNotification;

public class SendSmsNotification : ISendSmsNotification
{
    private string fromPhoneNumber = "50002710063488";

    private static string _melliPayamkPassword = "45089b4c-02ea-4ff1-8746-b4a3c029d08e";
    private static string _melliPayamkUsername = "09120655488";
    private static int _melliPayamkBodyIdForOTP = 274619;
    private static int _melliPayamkBodyIdForOrder = 279240;

    public ReturnResult<bool> SendVerificationCode(string phoneNumber, string code)
    {
        var restClient = new RestClient(_melliPayamkUsername, _melliPayamkPassword);
        var res = restClient.SendByBaseNumber(code, phoneNumber, _melliPayamkBodyIdForOTP);
        return ResultFactory.GetGoodResult();
    }

    public ReturnResult<bool> SendOrderToCafe(string phoneNumber, string coffeeName, string fullName, string userPhoneNumber, EstimateDelivery estimate)
    {
        try
        {
            var restClient = new RestClient(_melliPayamkUsername, _melliPayamkPassword);
            var tempCoffeeName = $"{coffeeName} {GetEstimateDelivery(estimate)}";
            var text = string.Join(';', new string[] { tempCoffeeName, fullName, userPhoneNumber });

            var res = restClient.SendByBaseNumber(text, phoneNumber, _melliPayamkBodyIdForOrder);
            var msgContext = $"به نام  {fullName} با شماره {userPhoneNumber} یک سفارش  {tempCoffeeName} ثبت شد";
            //var res = restClient.Send(phoneNumber, fromPhoneNumber, msgContext, false);
            msgContext = msgContext + $"\r\n به شماره {phoneNumber}";
            restClient.Send("09120655488", fromPhoneNumber, msgContext, false);
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

    private static string GetEstimateDelivery(EstimateDelivery estimate)
    {
        return estimate == EstimateDelivery.FiveMin ? "5 دقیقه" : "10 دقیقه";
    }

    public ReturnResult<bool> SendDefaultMsgForCafe(string phoneNumber, string msgContext)
    {
        try
        {
            var restClient = new RestClient(_melliPayamkUsername, _melliPayamkPassword);
            restClient.Send(phoneNumber, fromPhoneNumber, msgContext, false);
            restClient.Send("09120655488", fromPhoneNumber, msgContext, false);
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
