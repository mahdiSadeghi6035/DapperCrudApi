namespace CRUD.Common;

public class OperationResult
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }

    public OperationResult()
    {
    }

    private OperationResult(bool isSuccess, string? message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static OperationResult Success() => new(true, string.Empty);
    public static OperationResult Failed(string message) => new(false, message);
}
