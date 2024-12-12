using Repository.Helper.Validations;
using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class UserCustomer
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(70)]
    public string FullName { get; set; } = string.Empty;
    [MaxLength(20)]
    [Required]
    public required string PhoneNumber { get; set; }
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;
    public bool IsPhoneNumberVerified { get; set; } = false;

    public List<Order> Orders { get; set; } = [];


    public static UserCustomer Create(string phoneNumber, string fullname)
    {
        phoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(phoneNumber);


        var res = new UserCustomer
        {
            PhoneNumber = phoneNumber,
            FullName = fullname,
            IsPhoneNumberVerified = true,
            Username = string.Empty
        };


        return res;
    }


}
