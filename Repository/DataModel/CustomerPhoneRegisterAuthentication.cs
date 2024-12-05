namespace Repository.DataModel;

public class CustomerPhoneRegisterAuthentication
{
    public Guid Id { get; set; }
    public Guid UserCustomerId { get; set; }
    public Guid CafeId { get; set; }
    public DateTime ExpireDate { get; set; }
}
