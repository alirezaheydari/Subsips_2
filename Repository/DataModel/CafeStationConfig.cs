using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class CafeStationConfig
{
    [Key]
    public Guid Id { get; set; }
    public Guid CafeId { get; set; }
    public string? DefaultSmsMsg { get; set; }
    public bool IsSendSms { get; set; }
    public bool IsActive { get; set; }
    public CafeStation Cafe { get; set; }


    public static CafeStationConfig Create(Guid cafeId, string defaultSmsMsg, bool isSendSms, bool IsActive)
    {
        var res = new CafeStationConfig() 
        {
            CafeId = cafeId,
            DefaultSmsMsg = defaultSmsMsg,
            IsSendSms = isSendSms,
            IsActive = IsActive,
        };



        return res;
    }
}
