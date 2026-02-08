using System.Security.Claims;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Responses.AuthResponses;
using Microsoft.AspNetCore.Http;

namespace InvoiceGenerator.Infrastructure.Authentication;

/// <summary>
/// Service for accessing the currently authenticated user's information from HTTP context claims.
/// </summary>
public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    /// <inheritdoc />
    public string? UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier)
                             ?? User?.FindFirstValue("sub");

    /// <inheritdoc />
    public string? Email => User?.FindFirstValue(ClaimTypes.Email)
                            ?? User?.FindFirstValue("email");

    /// <inheritdoc />
    public string? Username => User?.FindFirstValue("preferred_username")
                               ?? User?.FindFirstValue(ClaimTypes.Name);

    /// <inheritdoc />
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    /// <inheritdoc />
    public IReadOnlyList<string> Roles
    {
        get
        {
            if (User is null)
            {
                return [];
            }

            // Keycloak stores realm roles in "realm_access.roles" claim (as JSON)
            // and resource roles in "resource_access.{client_id}.roles"
            // We also check standard role claims
            var roles = new List<string>();

            // Standard role claims
            roles.AddRange(User.FindAll(ClaimTypes.Role).Select(c => c.Value));
            roles.AddRange(User.FindAll("role").Select(c => c.Value));

            return roles.Distinct().ToList();
        }
    }

    /// <inheritdoc />
    public bool IsInRole(string role)
    {
        ArgumentNullException.ThrowIfNull(role);
        return User?.IsInRole(role) ?? false;
    }

    /// <inheritdoc />
    public CurrentUserResponse? GetCurrentUser()
    {
        if (!IsAuthenticated || UserId is null)
        {
            return null;
        }

        return new CurrentUserResponse
        {
            Id = UserId,
            Username = Username ?? string.Empty,
            Email = Email ?? string.Empty,
            FirstName = User?.FindFirstValue("given_name") ?? User?.FindFirstValue(ClaimTypes.GivenName),
            LastName = User?.FindFirstValue("family_name") ?? User?.FindFirstValue(ClaimTypes.Surname),
            EmailVerified = bool.TryParse(User?.FindFirstValue("email_verified"), out var verified) && verified,
            Roles = Roles
        };
    }
}
