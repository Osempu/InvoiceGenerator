using InvoiceGenerator.Core.Responses;
using InvoiceGenerator.Core.Responses.Errors;
using InvoiceGenerator.Core.Responses.ResultType;

namespace InvoiceGenerator.API.Extensions;

public static class ResultExtensions
{
    /// <summary>
    /// Converts a failed Result to a ProblemDetails response.
    /// </summary>
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot convert a successful result to problem details.");
        }

        return result.Error!.ToProblemDetails();
    }

    /// <summary>
    /// Converts an Error to a ProblemDetails response.
    /// </summary>
    public static IResult ToProblemDetails(this Error error)
    {
        return Results.Problem(
            statusCode: GetStatusCode(error.ErrorType),
            title: GetTitle(error.ErrorType),
            detail: error.Description,
            extensions: new Dictionary<string, object?>
            {
                {"errors", new [] {error}}
            });
    }

    /// <summary>
    /// Pattern matches on a Result, executing the appropriate function based on success or failure.
    /// </summary>
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Error, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error!);
    }

    /// <summary>
    /// Pattern matches on a Result{T}, executing the appropriate function based on success or failure.
    /// </summary>
    public static TOut Match<T, TOut>(
        this Result<T> result,
        Func<T, TOut> onSuccess,
        Func<Error, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error!);
    }

    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.NotFound => "Not Found",
            ErrorType.Validation => "Bad Request",
            ErrorType.Conflict => "Conflict",
            ErrorType.Unauthorized => "Unauthorized",
            _ => "Internal Server Error"
        };
}
