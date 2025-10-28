namespace InvoiceGenerator.Core.Responses.Errors;

public sealed record Error
{
    public Error(string Code, string Description, ErrorType errorType, IEnumerable<Error>? Errors = null)
    {
        this.Code = Code;
        this.Description = Description;
        this.ErrorType = errorType;
    }

    public string Code { get; set; }
    public string Description { get; set; }
    public ErrorType ErrorType { get; set; }

    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure, null);

    public static Error NotFound(string code, string description)
        => new(code, description, ErrorType.NotFound);
    public static Error Validation(string code, string desciption, IEnumerable<Error> errors)
        => new(code, desciption, ErrorType.Validation, errors);

    public static Error Conflict(string code, string description)
        => new(code, description, ErrorType.Conflict);

    public static Error Failure(string code, string description)
        => new(code, description, ErrorType.Failure);

    public static Error NotFound<T>(int id) => new Error("NotFound", $"{GetRecordName<T>()} with id: {id} was not found", ErrorType.NotFound);

    public static Error ValidationError<T>(IEnumerable<Error> errors)
    {
        return new Error($"{GetRecordName<T>()}.Validation.Error", "One or more validation errors ocurred", ErrorType.Validation, errors);
    }

    private static string GetRecordName<T>() =>
        typeof(T).Name.Replace("Dto", string.Empty)
                    .Replace("Request", string.Empty)
                    .Replace("Response", string.Empty);

}

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3
}