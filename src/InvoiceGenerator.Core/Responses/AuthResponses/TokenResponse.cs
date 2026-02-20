namespace InvoiceGenerator.Core.Responses.AuthResponses;

/// <summary>
/// Response DTO containing authentication tokens.
/// </summary>
public sealed record TokenResponse
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public required int ExpiresIn { get; init; }
    public required int RefreshExpiresIn { get; init; }
    public required string TokenType { get; init; }
}
