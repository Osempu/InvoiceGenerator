using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Core.Requests.AuthRequests;

/// <summary>
/// Request DTO for logout operation.
/// </summary>
public sealed record LogoutRequest
{
    [Required]
    public required string RefreshToken { get; init; }
}
