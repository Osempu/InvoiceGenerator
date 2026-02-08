using InvoiceGenerator.Core.Responses.AuthResponses;

namespace InvoiceGenerator.Core.Contracts;

/// <summary>
/// Service contract for accessing the currently authenticated user's information.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the current user's unique identifier (subject claim).
    /// </summary>
    string? UserId { get; }

    /// <summary>
    /// Gets the current user's email address.
    /// </summary>
    string? Email { get; }

    /// <summary>
    /// Gets the current user's username (preferred_username claim).
    /// </summary>
    string? Username { get; }

    /// <summary>
    /// Gets whether the current request is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Gets all roles assigned to the current user.
    /// </summary>
    IReadOnlyList<string> Roles { get; }

    /// <summary>
    /// Checks if the current user has a specific role.
    /// </summary>
    /// <param name="role">The role to check.</param>
    /// <returns>True if the user has the role.</returns>
    bool IsInRole(string role);

    /// <summary>
    /// Gets the complete current user information from claims.
    /// </summary>
    /// <returns>Current user details or null if not authenticated.</returns>
    CurrentUserResponse? GetCurrentUser();
}
