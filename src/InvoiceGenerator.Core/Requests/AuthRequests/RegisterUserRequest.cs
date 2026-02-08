using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Core.Requests.AuthRequests;

/// <summary>
/// Request DTO for user registration.
/// </summary>
public sealed record RegisterUserRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    [MinLength(8)]
    public required string Password { get; init; }

    [Required]
    public required string FirstName { get; init; }

    [Required]
    public required string LastName { get; init; }

    public string? Username { get; init; }
}
