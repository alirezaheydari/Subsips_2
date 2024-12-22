using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class SubwayStation
{
    [Key]
    public Guid Id { get; set; }
    [StringLength(128)]
    public required string Name { get; set; }
    public byte Line { get; set; }
    [StringLength(256)]
    public string? Description { get; set; }
    public Guid? CafeId { get; set; }


    public CafeStation? Cafe { get; set; }
}

public enum SubwayLines
{
    [Display(Name = "خط 1")]
    Line_1 = 1,
    [Display(Name = "خط 2")]
    Line_2 = 2,
    [Display(Name = "خط 3")]
    Line_3 = 3,
    [Display(Name = "خط 4")]
    Line_4 = 4,
    [Display(Name = "خط 5")]
    Line_5 = 5,
    [Display(Name = "خط 6")]
    Line_6 = 6,
    [Display(Name = "خط 7")]
    Line_7 = 7,
}
