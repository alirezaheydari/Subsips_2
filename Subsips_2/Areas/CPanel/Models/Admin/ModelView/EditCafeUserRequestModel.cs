namespace Subsips_2.Areas.CPanel.Models.Admin.ModelView;

public class EditCafeUserRequestModel
{
    public string UserId { get; set; }
    public Guid CafeId { get; set; }

    public bool IsActive { get; set; }
    public bool IsOwner { get; set; }
}
