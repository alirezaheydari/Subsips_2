using Repository.Helper;
using Repository.Helper.Validations;
using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class TimeSheetShiftCafe
{
    [Key]
    public Guid Id { get; set; }
    public Guid CafeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    [StringLength(20)]
    public required string PhoneNumber { get; set; }


    public static ReturnResult<TimeSheetShiftCafe> Create(Guid cafeId,
                                                          DateTime startTime,
                                                          DateTime endTime,
                                                          bool isActive,
                                                          string phoneNumber,
                                                          DayOfWeek dayOfWeek)
    {

        phoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(phoneNumber);
        if (!PhoneNumberValidation.IsValid(phoneNumber))
            return ResultFactory.GetBadResult<TimeSheetShiftCafe>(new string[]
            {
                "Model is not valid"
            });

        if (startTime >= endTime)
            return ResultFactory.GetBadResult<TimeSheetShiftCafe>(new string[]
            {
                "Model is not valid"
            });

        var res = new TimeSheetShiftCafe()
        {
            Id = Guid.NewGuid(),
            CafeId = cafeId,
            DayOfWeek = dayOfWeek,
            EndTime = endTime,
            StartTime = startTime,
            IsActive = isActive,
            PhoneNumber = phoneNumber
        };



        return ResultFactory.GetGoodResult(res);
    }





    public CafeStation Cafe { get; set; }
}


