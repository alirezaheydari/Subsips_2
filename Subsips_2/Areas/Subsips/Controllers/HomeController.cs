using Microsoft.AspNetCore.Mvc;
using Repository.Helper.TockenGenerator;
using Subsips_2.Areas.Subsips.Models.UserCustomer;
using Subsips_2.BusinessLogic.SendNotification;
using Subsips_2.BusinessLogic.SubwayStation;
using Subsips_2.BusinessLogic.UserCustomer;

namespace Subsips_2.Areas.Subsips.Controllers;

[Area("Subsips")]
public class HomeController : Controller
{
    private readonly ISendSmsNotification smsSender;
    private readonly ISubwayStationRepository subwayStation;
    private readonly IUserCustomerRepository userCustomer;
    private readonly ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegisterAuthentication;
    private readonly IVerificationCodeRepository verificationCode;

    public HomeController(ISubwayStationRepository subwayStation, IVerificationCodeRepository verificationCode, ISendSmsNotification smsSender, IUserCustomerRepository userCustomer, ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegisterAuthentication)
    {
        this.subwayStation = subwayStation;
        this.verificationCode = verificationCode;
        this.smsSender = smsSender;
        this.userCustomer = userCustomer;
        this.customerPhoneRegisterAuthentication = customerPhoneRegisterAuthentication;
    }
    public IActionResult Index()
    {
        var stations = subwayStation.GetViewModelAll();
        if (stations.IsFailed)
            return View("Error");

        return View(stations.Result);
    }

    public IActionResult UserRegister()
    {
        
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> UserRegisterAsync([FromForm] UserRegisterFormRequestModel formRequest)
    {
        if (!formRequest.IsPhoneNumberValid())
            return NotFound();


        var isVerified = await verificationCode.IsVerfied(formRequest.PhoneNumber, formRequest.OtpCode);
        if ((!isVerified))
            return NotFound();
        var currentCustomer = (await userCustomer.Add(formRequest.PhoneNumber, string.Empty)).Result;


        var registerId = await customerPhoneRegisterAuthentication.Add(new Guid("B7C7D162-8871-4930-D55B-08DD08101897"), currentCustomer.Id);


        SetRegisterId("RID", registerId.Result.ToString());
        return RedirectToAction("Index");
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
