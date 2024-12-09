using Microsoft.AspNetCore.Mvc;
using Repository.DataModel;
using Repository.Helper.TockenGenerator;
using Subsips_2.Areas.Subsips.Models.UserCustomer;
using Subsips_2.BusinessLogic.SendNotification;

namespace Subsips_2.Areas.Subsips.Controllers;

[Area("Subsips")]
public class UserCustomerController : Controller
{
    private readonly ISendSmsNotification smsSender;
    private readonly IVerificationCodeRepository verificationCode;

    public UserCustomerController(ISendSmsNotification smsSender, IVerificationCodeRepository verificationCode)
    {
        this.smsSender = smsSender;
        this.verificationCode = verificationCode;
    }
    public IActionResult PhoneNumberRegister(Guid coffeeId, Guid orderId)
    {
        return View(new PhoneNumberRegisterViewModel
        {
            CoffeeId = coffeeId,
            OrderId = orderId
        });
    }
    //[HttpPost]
    //public async Task<IActionResult> PhoneNumberRegister(PhoneNumberRegisterFormRequestModel formRequest)
    //{

    //}
    public  async Task<IActionResult> SendOtp(string phoneNumber)
    {
        //if (!formRequest.IsValid())
        //{
        //    ViewData["ErrorMsg"] = "لطفا شماره تلفن را صحیح وارد نمایید";
        //    return View("PhoneNumberRegister", new PhoneNumberRegisterViewModel
        //    {
        //        CoffeeId = formRequest.CoffeeId,
        //        OrderId = formRequest.OrderId
        //    });
        //}
        var otpCode = DigitVerificationCodeGenerator.GetDigitsConfirmationCode();
        smsSender.SendVerificationCode(phoneNumber, otpCode);
        await verificationCode.Add(phoneNumber, otpCode);

        return View();
    }
}
