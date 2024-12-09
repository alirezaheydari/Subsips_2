using Repository.Helper;

namespace Subsips_2.BusinessLogic.SendNotification;

public interface IVerificationCodeRepository
{
    public Task<ReturnResult<bool>> Add(string phoneNumber, string code);
    public Task<bool> IsVerfied(string phoneNumber, string code);
}
