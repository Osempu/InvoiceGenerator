using InvoiceGenerator.Core.Responses.Errors;
using InvoiceGenerator.Core.Responses.ResultType;

namespace InvoiceGenerator.Core.Responses;

public class Result<T> : Result
{
    private readonly T? _value;


    private Result(T value) : base()
    {
        _value = value;
    }

    private Result(Error error) : base(error)
    {
        if (error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
    }

    public bool IsFailure => !IsSuccess;
    public T Value
    {
        get
        {
            return _value!;
        }
        private init => _value = value;
    }

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Fail(Error error) => new(error);
}
