using System.Text.RegularExpressions;

namespace Repository.Helper.Validations;

public class PhoneNumberValidation
{
    public static bool IsValid(string phoneNumber)
    {
        if (phoneNumber is null)
            return false;

        return Regex.IsMatch(phoneNumber, motif);
    }

    public static string GetPhoneNumberWithoutZero(string phoneNumber)
    {
        phoneNumber = phoneNumber.StartsWith('0') ? phoneNumber.Remove(0, 1) : phoneNumber;

        if (IsValid(phoneNumber))
            return phoneNumber;

        return string.Empty;
    }


    private const string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

}
