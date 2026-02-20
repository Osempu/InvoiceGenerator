using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Core.Requests.AuthRequests;

/// <summary>
/// Request DTO for refreshing an access token.
/// </summary>
public sealed record RefreshTokenRequest
{
    [Required]
    public required string RefreshToken { get; init; }
}
