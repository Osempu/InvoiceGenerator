using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Core.Requests.AuthRequests;

/// <summary>
/// Request DTO for user login.
/// </summary>
public sealed record LoginUserRequest
{
    [Required]
    public required string Username { get; init; }

    [Required]
    public required string Password { get; init; }
}
