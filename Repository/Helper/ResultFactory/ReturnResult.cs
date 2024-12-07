namespace Repository.Helper;

public class ReturnResult<T>
{
    public bool IsSuccess { get; set; }
    public bool IsFailed { get => !IsSuccess; }
    public IEnumerable<string>? Errors { get; set; } = null;
    public T? Result { get; set; }
}
