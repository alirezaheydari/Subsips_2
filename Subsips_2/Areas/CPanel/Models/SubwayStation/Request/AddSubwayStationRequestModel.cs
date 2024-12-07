using Repository.DataModel;
using System.ComponentModel.DataAnnotations;

namespace Subsips_2.Areas.CPanel.Models.SubwayStation.Request;

public class AddSubwayStationRequestModel
{
    [Display(Name = "نام ایستگاه مترو")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "خط مترو")]
    public SubwayLines Line { get; set; }
    [Display(Name = "توضیحات")]
    public string Description { get; set; } = string.Empty;
}
