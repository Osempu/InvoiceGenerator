using InvoiceGenerator.Core.Responses.Errors;

namespace InvoiceGenerator.Core.Responses.ResultType;

public static class ResultExtensions
{
    public static T Match<T, TValue>(this Result<TValue> result, Func<TValue, T> success, Func<Error, T> failure) {
        return result.IsSuccess ? success(result.Value) : failure(result.Error!);
    }

    public static T Match<T>(this Result result, Func<T> success, Func<Error, T> failure) {
        var res = result.IsSuccess ? success() : failure(result.Error!);
        return res;
    }
}
