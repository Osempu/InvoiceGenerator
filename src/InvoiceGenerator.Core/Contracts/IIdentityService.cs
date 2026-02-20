using InvoiceGenerator.Core.Requests.AuthRequests;
using InvoiceGenerator.Core.Responses;
using InvoiceGenerator.Core.Responses.AuthResponses;
using InvoiceGenerator.Core.Responses.ResultType;

namespace InvoiceGenerator.Core.Contracts;

/// <summary>
/// Service contract for user identity operations (authentication, registration, etc.).
/// </summary>
public interface IIdentityService
{
    /// <summary>
    /// Registers a new user in the identity provider.
    /// </summary>
    /// <param name="request">The registration details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The newly created user's ID on success, or an error on failure.</returns>
    Task<Result<string>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Authenticates a user and returns access/refresh tokens.
    /// </summary>
    /// <param name="request">The login credentials.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Token response on success, or an error on failure.</returns>
    Task<Result<TokenResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Refreshes an access token using a refresh token.
    /// </summary>
    /// <param name="request">The refresh token request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>New token response on success, or an error on failure.</returns>
    Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Logs out a user by invalidating their refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to invalidate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Success or an error on failure.</returns>
    Task<Result> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default);
}
