using Microsoft.AspNetCore.Mvc;
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
    [HttpPost]
    public async Task<IActionResult> PhoneNumberRegister(PhoneNumberRegisterFormRequestModel formRequest)
    {
        if (!formRequest.IsValid())
            return NotFound();


        var isVerified = await verificationCode.IsVerfied(formRequest.PhoneNumber, formRequest.OtpCode);
        if (!formRequest.IsValid() || (!isVerified))
            return NotFound();
        // TODO : 1- create user 
        // TODO : 2- assign cookie
        // TODO : 3- create order
        // TODO : 4- redirect to show status
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> SendOtp([FromBody] SendOtpFormRequestModel model)
    {

        if (!model.IsPhoneNumberValid())
        {
            return NotFound();
        }
        var otpCode = DigitVerificationCodeGenerator.GetDigitsConfirmationCode();
        var resOfSaveCode = await verificationCode.Add(model.PhoneNumber, otpCode);
        if (resOfSaveCode.IsSuccess)
            smsSender.SendVerificationCode(model.PhoneNumber, otpCode);
        return Ok();
    }
}
