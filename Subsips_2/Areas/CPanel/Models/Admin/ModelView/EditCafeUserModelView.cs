namespace Subsips_2.Areas.CPanel.Models.Admin.ModelView;

public class EditCafeUserModelView
{
    public string Email { get; set; }
    public string UserId { get; set; }


    public List<CafeStationInfoViewModel> Cafes { get; set; }

}


public class CafeStationInfoViewModel
{
    public Guid CafeId { get; set; }
    public string CafeName { get; set; }
    public string StationName { get; set; }
}