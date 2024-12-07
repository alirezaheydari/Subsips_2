namespace Repository.Helper;

public static class ResultFactory
{
    public static ReturnResult<T> GetGoodResult<T>(T result)
    {
        return new ReturnResult<T> { IsSuccess = true, Result = result };
    }
    public static ReturnResult<T> GetBadResult<T>(IEnumerable<string> errors)
    {
        return new ReturnResult<T> { IsSuccess = false, Errors = errors };
    }

    public static ReturnResult<bool> GetGoodResult()
    {
        return new ReturnResult<bool> { IsSuccess = true };
    }
    public static ReturnResult<bool> GetBadResult(IEnumerable<string> errors)
    {
        return new ReturnResult<bool> { IsSuccess = false, Errors = errors };
    }
}
