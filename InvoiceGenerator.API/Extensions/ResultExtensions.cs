using InvoiceGenerator.Core.Responses.Errors;
using InvoiceGenerator.Core.Responses.ResultType;

namespace InvoiceGenerator.API.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            statusCode: GetStatusCode(result.Error!.ErrorType),
            title: GetTitle(result.Error.ErrorType),
            detail: result.Error.Description,
            extensions: new Dictionary<string, object?>
            {
                {"errors", new [] {result.Error}}
            });
    }

    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.NotFound => "Not Found",
            ErrorType.Validation => "Bad Request",
            ErrorType.Conflict => "Conflict",
            _ => "Internal Server Error"
        };
}
