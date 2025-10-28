using System.Text.Json.Serialization;
using InvoiceGenerator.Core.Responses.Errors;

namespace InvoiceGenerator.Core.Responses.ResultType;

public class Result
{
    protected Result()
    {
        IsSuccess = true;
        Error = Error.None;
    }

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public bool IsSuccess { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Error? Error { get; set; }

    public static implicit operator Result(Error error) => new(error);
    public static Result Success() => new();
    public static Result Failure(Error error) => new(error);
}
