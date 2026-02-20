namespace InvoiceGenerator.Core.Responses.AuthResponses;

/// <summary>
/// Response DTO containing the current user's information.
/// </summary>
public sealed record CurrentUserResponse
{
    public required string Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public bool EmailVerified { get; init; }
    public IReadOnlyList<string> Roles { get; init; } = [];
}
