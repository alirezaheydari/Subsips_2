namespace Subsips_2.Areas.CPanel.Models.CafeStation.ModelView;

public class ExclusiveUsersModelView
{
    public List<ExclusiveUsersItemModelView> Items { get; set; }
}
public class ExclusiveUsersItemModelView
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
}