using System.Security.Cryptography;

namespace Repository.Helper.TockenGenerator;

public static class DigitVerificationCodeGenerator
{
    public static string GetDigitsConfirmationCode()
    {

        byte[] randomBytes = new byte[6]; // 6 bytes for generating a random integer
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        // Convert to a positive number and scale it to the desired range
        int randomNumber = BitConverter.ToInt32(randomBytes, 0) & 0x7FFFFFFF; // Ensure positive integer
        int min = (int)Math.Pow(10, 6 - 1);
        int max = (int)Math.Pow(10, 6) - 1;

        int code = randomNumber % (max - min + 1) + min;
        return code.ToString();
    }
}
