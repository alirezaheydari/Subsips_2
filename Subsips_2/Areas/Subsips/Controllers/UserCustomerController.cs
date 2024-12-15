using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Repository.DataModel;
using Repository.Helper.TockenGenerator;
using Subsips_2.Areas.Subsips.Models.UserCustomer;
using Subsips_2.BusinessLogic.Order;
using Subsips_2.BusinessLogic.SendNotification;
using Subsips_2.BusinessLogic.UserCustomer;
using System.Security.Cryptography;

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
        var currentCustomer = (await userCustomer.Add(formRequest.PhoneNumber, formRequest.FullName)).Result;


        var registerId = await customerPhoneRegisterAuthentication.Add(formRequest.CafeId, currentCustomer.Id);

        SetRegisterId("RID", registerId.Result.ToString());

        await orderRepository.MakeNewOrder(formRequest.OrderId, string.Empty, formRequest.CafeId, formRequest.CoffeeId, currentCustomer.Id);

        return RedirectToAction("ShowStatusOrder", new { orderId = formRequest.OrderId });
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

    public async Task<IActionResult> MakeOrder(Guid orderId, Guid cafeId, Guid coffeeId)
    {
        var request = ControllerContext?.HttpContext?.Request;
        var rid = request?.Cookies?.FirstOrDefault(x => x.Key == "RID").Value;
        var guidRid = new Guid(rid);
        var regiseterRecordResult = customerPhoneRegisterAuthentication.Get(guidRid, cafeId);

        if (regiseterRecordResult is null || regiseterRecordResult.IsFailed)
            return NotFound();

        var regiseterRecord = regiseterRecordResult.Result;
        if (regiseterRecord is null)
            return NotFound();


        var currentCustomerResult = userCustomer.Find(regiseterRecord.UserCustomerId);
        if (currentCustomerResult.IsFailed)
            return NotFound();

        SetRegisterId("RID", regiseterRecord.UserCustomerId.ToString());

        var resutlOrder = await orderRepository.MakeNewOrder(orderId, string.Empty, cafeId, coffeeId, currentCustomerResult.Result.Id);

        if (resutlOrder.IsFailed)
            return NotFound();

        return RedirectToAction("ShowStatusOrder", new { orderId });
    }

    public IActionResult ShowStatusOrder(Guid orderId)
    {
        var currentOrder = orderRepository.GetShowStatusOrderModelView(orderId);
        if (currentOrder is null || currentOrder.IsFailed)
            return NotFound();



        return View(currentOrder.Result);
    }

    private void SetRegisterId(string key, string value)
    {
        CookieOptions option = new CookieOptions();

        option.Expires = DateTime.Now.AddDays(10);

        Response.Cookies.Append(key, value, option);
    }
}
