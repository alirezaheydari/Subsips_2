using Repository.DataModel;
using Repository.Helper;
using Repository.Helper.Validations;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.SendNotification;

public class VerificationCodeRepository : IVerificationCodeRepository
{
    private readonly Subsips_2Context _context;
    public VerificationCodeRepository(Subsips_2Context _context)
    {
        this._context = _context;
    }

    public async Task<ReturnResult<bool>> Add(string phoneNumber, string code)
    {
        var phoneNumberWithoutZero = PhoneNumberValidation.GetPhoneNumberWithoutZero(phoneNumber);

        var lastVerify = GetLastVerificationRecord(phoneNumberWithoutZero);

        if (lastVerify is not null && (!lastVerify.IsExpired()))
            return ResultFactory.GetGoodResult();

        var res = VerificationCode.Create(phoneNumber, code);

        _context.VerificationCodes.Add(res);

        await _context.SaveChangesAsync();

        return ResultFactory.GetGoodResult();
    }

    private VerificationCode? GetLastVerificationRecord(string phoneNumberWithoutZero)
    {
        return _context.VerificationCodes.Where(x => x.PhoneNumber == phoneNumberWithoutZero).OrderByDescending(x => x.CreateTime)?.Take(1).FirstOrDefault();
    }

    public async Task<bool> IsVerfied(string phoneNumber, string code)
    {
        var phoneNumberWithoutZero = PhoneNumberValidation.GetPhoneNumberWithoutZero(phoneNumber);

        var lastVerify = _context.VerificationCodes.Where(x => x.PhoneNumber == phoneNumberWithoutZero).OrderByDescending(x => x.CreateTime)?.Take(1).FirstOrDefault();

        if (lastVerify is null)
            return false;

        if (!lastVerify.IsVerified(code))
            return false;

        lastVerify.Verified = true;

        await _context.SaveChangesAsync();

        return true;
    }

}
