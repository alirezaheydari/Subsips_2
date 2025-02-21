﻿using Microsoft.AspNetCore.Mvc;
using Repository.DataModel;
using Repository.Helper;
using Repository.Helper.TockenGenerator;
using Subsips_2.Areas.Subsips.Models.UserCustomer;
using Subsips_2.Areas.Subsips.Models.UserCustomer.FormRequest;
using Subsips_2.BusinessLogic.Cafe;
using Subsips_2.BusinessLogic.CoffeeCups;
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
    private readonly ITimeSheetShiftCafeRepository timeSheetRepository;
    private readonly ICafeStationRepository cafeRepository;
    private readonly ICoffeeCupRepository coffeeCupRepository;

    public UserCustomerController(ISendSmsNotification smsSender, IVerificationCodeRepository verificationCode, IUserCustomerRepository userCustomer, ICustomerPhoneRegisterAuthenticationRepository customerPhoneRegisterAuthentication, IOrderRepository orderRepository, ICafeStationRepository cafeRepository, ICoffeeCupRepository coffeeCupRepository, ITimeSheetShiftCafeRepository timeSheetRepository)
    {
        this.smsSender = smsSender;
        this.verificationCode = verificationCode;
        this.userCustomer = userCustomer;
        this.customerPhoneRegisterAuthentication = customerPhoneRegisterAuthentication;
        this.orderRepository = orderRepository;
        this.cafeRepository = cafeRepository;
        this.coffeeCupRepository = coffeeCupRepository;
        this.timeSheetRepository = timeSheetRepository;
    }
    public IActionResult GetUserOrders()
    {

        var request = ControllerContext?.HttpContext?.Request;
        var rid = request?.Cookies?.FirstOrDefault(x => x.Key == "RID").Value ?? string.Empty;
        var guidRid = new Guid(rid);
        var regiseterRecordResult = customerPhoneRegisterAuthentication.Get(guidRid);

        if (regiseterRecordResult is null || regiseterRecordResult.IsFailed)
            return View(new GetUserOrdersViewModel
            {
                Items = new List<UserOrderItem>()
            });

        var regiseterRecord = regiseterRecordResult.Result;
        if (regiseterRecord is null)
            return View(new GetUserOrdersViewModel
            {
                Items = new List<UserOrderItem>()
            });

        var currentCustomerResult = userCustomer.Find(regiseterRecord.UserCustomerId);
        if (currentCustomerResult.IsFailed)
            return View(new GetUserOrdersViewModel
            {
                Items = new List<UserOrderItem>()
            });

        var model = orderRepository.GetAllOrdersOfCustomer(currentCustomerResult.Result.Id);


        return View(new GetUserOrdersViewModel
        {
            Items = model.Result
        });
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


        await MakeOrder(formRequest.OrderId, formRequest.CoffeeId, formRequest.CafeId, registerId.Result, currentCustomer, formRequest.Description, formRequest.EstimateDeliver);

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
    [HttpPost]
    public async Task<IActionResult> MakeOrder([FromBody]MakeOrderFormRequestModel model)
    {
        var request = ControllerContext?.HttpContext?.Request;
        var rid = request?.Cookies?.FirstOrDefault(x => x.Key == "RID").Value;
        var guidRid = new Guid(rid);
        var regiseterRecordResult = customerPhoneRegisterAuthentication.Get(guidRid);

        if (regiseterRecordResult is null || regiseterRecordResult.IsFailed)
            return NotFound();

        var regiseterRecord = regiseterRecordResult.Result;
        if (regiseterRecord is null)
            return NotFound();

        var currentCustomerResult = userCustomer.Find(regiseterRecord.UserCustomerId);
        if (currentCustomerResult.IsFailed)
            return NotFound();

        await MakeOrder(model.OrderId, model.CoffeeId, model.CafeId, regiseterRecord.Id, currentCustomerResult.Result, model.Description, model.EstimateDeliver);

        return RedirectToAction("ShowStatusOrder", new { model.OrderId });
    }

    private async Task<IActionResult> MakeOrder(Guid orderId, Guid coffeeId, Guid cafeId, Guid regiseterId, UserCustomer customer, string description, EstimateDelivery estimate)
    {
        SetRegisterId("RID", regiseterId.ToString());
        await customerPhoneRegisterAuthentication.ReloadTockenRegisterationAsync(regiseterId);
        var resutlOrder = await orderRepository.MakeNewOrder(orderId, description, cafeId, coffeeId, customer.Id, estimate);

        if (resutlOrder.IsFailed)
            return NotFound();


        var coffeeInfo = coffeeCupRepository.FindCoffeeAndCafeInfo(coffeeId);

        var regiseterRecordResult = customerPhoneRegisterAuthentication.Get(regiseterId);

        if (regiseterRecordResult is null || regiseterRecordResult.IsFailed)
            return NotFound();

        var regiseterRecord = regiseterRecordResult.Result;
        if (regiseterRecord is null)
            return NotFound();

        var shiftPhoneNumbers = timeSheetRepository.GetPhoneNumbers(cafeId);
        if (shiftPhoneNumbers is not null && shiftPhoneNumbers.Any())
        {
            shiftPhoneNumbers.ForEach(x =>
            {
                smsSender.SendOrderToCafe(x, coffeeInfo.Result.CoffeeName, customer.FullName, customer.PhoneNumber, estimate);
            });
        }


        smsSender.SendOrderToCafe(coffeeInfo.Result.CafePhoneNumber, coffeeInfo.Result.CoffeeName, customer.FullName, customer.PhoneNumber , estimate);
        return Ok();
    }

    public IActionResult ShowStatusOrder(Guid orderId)
    {
        var currentOrder = orderRepository.GetShowStatusOrderModelView(orderId);
        if (currentOrder is null || currentOrder.IsFailed)
            return NotFound();
        var shiftPhoneNumbers = timeSheetRepository.GetPhoneNumbers(currentOrder.Result.CafeId);
        if (shiftPhoneNumbers is not null && shiftPhoneNumbers.Any())
        {
            currentOrder.Result.CafePhoneNumber = shiftPhoneNumbers.First();
        }



        return View(currentOrder.Result);
    }

    private void SetRegisterId(string key, string value)
    {
        CookieOptions option = new CookieOptions();

        option.Expires = DateTime.Now.AddDays(10);

        Response.Cookies.Append(key, value, option);
    }
}
