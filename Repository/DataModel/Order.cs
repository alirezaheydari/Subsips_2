using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class Order
{
    public Guid Id { get; set; }
    public byte Status { get; set; }
    public byte? EstimateDeliver { get; set; }
    public string? Description { get; set; }
    public Guid CafeId { get; set; }
    public Guid? StationId { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    //public IEnumerable<Guid> CoffeeIds { get; set; } = [];

    public static Order Create(Guid id, string description, Guid cafeId, Guid customerId, EstimateDelivery  estimate, Guid stationId)
    {
        var resutl = new Order();

        resutl.Id = id;
        resutl.Description = description ?? string.Empty;
        resutl.CreateDate = DateTime.Now;
        resutl.CafeId = cafeId;
        resutl.CustomerId = customerId;
        resutl.Status = (byte)OrderStatus.OnProcessed;
        resutl.EstimateDeliver = (byte)estimate;
        resutl.StationId = stationId;

        return resutl;
        
    }

    public CafeStation Cafe { get; set; }
    //public List<CoffeeCup> Coffee { get; set; } = [];
    public List<CoffeeCupOrder> CoffeeCupOrders { get; set; } = [];
    public UserCustomer Customer { get; set; }
}




public enum EstimateDelivery
{
    FiveMin = 5,
    TenMin = 10
};
public enum OrderStatus
{
    [Display(Name = "درحال پردازش")]
    OnProcessed,
    [Display(Name = "ثبت")]
    Ordered,
    [Display(Name = "تایید")]
    Confirmed,
    [Display(Name = "رد")]
    Rejected,
    [Display(Name = "کنسل")]
    Canceled,
    [Display(Name = "آماده")]
    Ready,
    [Display(Name = "تکمیل")]
    Completed
}