using Microsoft.AspNetCore.Mvc;
using Repository.Helper.TockenGenerator;
using Subsips_2.Areas.Subsips.Models.UserCustomer;
using Subsips_2.BusinessLogic.Order;
using Subsips_2.BusinessLogic.SendNotification;
using Subsips_2.BusinessLogic.UserCustomer;

namespace Subsips_2.Areas.Subsips.Controllers;

[Area("Subsips")]
public class UserCustomerController : Controller
{
    private readonly ISendSmsNotification smsSender;
    private readonly IVerificationCodeRepository verificationCode;
    private readonly IUserCustomerRepository userCustomer;
    private readonly ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegisterAuthentication;
    private readonly IOrderRepository orderRepository;

    public UserCustomerController(ISendSmsNotification smsSender, IVerificationCodeRepository verificationCode, IUserCustomerRepository userCustomer, ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegisterAuthentication, IOrderRepository orderRepository)
    {
        this.smsSender = smsSender;
        this.verificationCode = verificationCode;
        this.userCustomer = userCustomer;
        this.customerPhoneRegisterAuthentication = customerPhoneRegisterAuthentication;
        this.orderRepository = orderRepository;
    }
    public IActionResult PhoneNumberRegister(Guid coffeeId, Guid orderId, Guid cafeId)
    {
        return View(new PhoneNumberRegisterViewModel
        {
            CoffeeId = coffeeId,
            OrderId = orderId,
            CafeId = cafeId
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

        var currentCustomer = (await userCustomer.Add(formRequest.PhoneNumber, formRequest.FullName)).Result;


        // TODO : 2- assign cookie

        var registerId = await customerPhoneRegisterAuthentication.Add(formRequest.CafeId, currentCustomer.Id);

        var request = ControllerContext?.HttpContext?.Request;

        request.Cookies.Append(new KeyValuePair<string, string>("RID", registerId.Result.ToString()));

        // TODO : 3- create order

        await orderRepository.MakeNewOrder(formRequest.OrderId, string.Empty, formRequest.CafeId, formRequest.CoffeeId, currentCustomer.Id);


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

    private void SetRegisterId(string key, string value)
    {
        CookieOptions option = new CookieOptions();

        option.Expires = DateTime.Now.AddDays(10);

        Response.Cookies.Append(key, value, option);
    }
}
