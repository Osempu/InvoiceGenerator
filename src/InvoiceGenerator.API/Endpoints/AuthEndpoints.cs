using Carter;
using InvoiceGenerator.API.Extensions;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Requests.AuthRequests;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceGenerator.API.Endpoints;

/// <summary>
/// Authentication endpoints for user registration, login, logout, and token management.
/// </summary>
public sealed class AuthEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth")
            .WithTags("Authentication")
            .WithOpenApi();

        group.MapPost("/register", RegisterAsync)
            .WithName("RegisterUser")
            .WithSummary("Register a new user")
            .WithDescription("Creates a new user account in the identity provider.")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapPost("/login", LoginAsync)
            .WithName("LoginUser")
            .WithSummary("Authenticate a user")
            .WithDescription("Authenticates a user and returns access and refresh tokens.")
            .Produces<Core.Responses.AuthResponses.TokenResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapPost("/refresh", RefreshTokenAsync)
            .WithName("RefreshToken")
            .WithSummary("Refresh access token")
            .WithDescription("Exchanges a refresh token for a new access token.")
            .Produces<Core.Responses.AuthResponses.TokenResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapPost("/logout", LogoutAsync)
            .WithName("LogoutUser")
            .WithSummary("Log out a user")
            .WithDescription("Invalidates the user's refresh token.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();

        group.MapGet("/me", GetCurrentUserAsync)
            .WithName("GetCurrentUser")
            .WithSummary("Get current user information")
            .WithDescription("Returns the currently authenticated user's profile information.")
            .Produces<Core.Responses.AuthResponses.CurrentUserResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .RequireAuthorization();
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    private static async Task<IResult> RegisterAsync(
        [FromBody] RegisterUserRequest request,
        IIdentityService identityService,
        ILogger<AuthEndpoints> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope("RegisterUser: {Email}", request.Email);
        logger.LogInformation("Processing user registration request");

        var result = await identityService.RegisterAsync(request, cancellationToken);

        return result.Match(
            onSuccess: userId =>
            {
                logger.LogInformation("User registered successfully with ID {UserId}", userId);
                return Results.Created($"/api/auth/users/{userId}", new { UserId = userId });
            },
            onFailure: error =>
            {
                logger.LogWarning("User registration failed: {ErrorCode} - {ErrorDescription}", error.Code, error.Description);
                return error.ToProblemDetails();
            });
    }

    /// <summary>
    /// Authenticates a user and returns tokens.
    /// </summary>
    private static async Task<IResult> LoginAsync(
        [FromBody] LoginUserRequest request,
        IIdentityService identityService,
        ILogger<AuthEndpoints> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope("LoginUser: {Username}", request.Username);
        logger.LogInformation("Processing login request");

        var result = await identityService.LoginAsync(request, cancellationToken);

        return result.Match(
            onSuccess: tokenResponse =>
            {
                logger.LogInformation("User {Username} logged in successfully", request.Username);
                return Results.Ok(tokenResponse);
            },
            onFailure: error =>
            {
                logger.LogWarning("Login failed for user {Username}: {ErrorCode}", request.Username, error.Code);
                return error.ToProblemDetails();
            });
    }

    /// <summary>
    /// Refreshes an access token using a refresh token.
    /// </summary>
    private static async Task<IResult> RefreshTokenAsync(
        [FromBody] RefreshTokenRequest request,
        IIdentityService identityService,
        ILogger<AuthEndpoints> logger,
        CancellationToken cancellationToken)
    {
        logger.LogDebug("Processing token refresh request");

        var result = await identityService.RefreshTokenAsync(request, cancellationToken);

        return result.Match(
            onSuccess: tokenResponse =>
            {
                logger.LogDebug("Token refreshed successfully");
                return Results.Ok(tokenResponse);
            },
            onFailure: error =>
            {
                logger.LogWarning("Token refresh failed: {ErrorCode}", error.Code);
                return error.ToProblemDetails();
            });
    }

    /// <summary>
    /// Logs out a user by invalidating their refresh token.
    /// </summary>
    private static async Task<IResult> LogoutAsync(
        [FromBody] LogoutRequest request,
        IIdentityService identityService,
        ILogger<AuthEndpoints> logger,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Processing logout request");

        var result = await identityService.LogoutAsync(request.RefreshToken, cancellationToken);

        return result.Match(
            onSuccess: () =>
            {
                logger.LogInformation("User logged out successfully");
                return Results.NoContent();
            },
            onFailure: error =>
            {
                logger.LogWarning("Logout failed: {ErrorCode}", error.Code);
                return error.ToProblemDetails();
            });
    }

    /// <summary>
    /// Gets the current authenticated user's information.
    /// </summary>
    private static IResult GetCurrentUserAsync(
        ICurrentUserService currentUserService,
        ILogger<AuthEndpoints> logger)
    {
        var user = currentUserService.GetCurrentUser();

        if (user is null)
        {
            logger.LogWarning("Attempted to get current user but no user is authenticated");
            return Results.Unauthorized();
        }

        logger.LogDebug("Returning current user info for {UserId}", user.Id);
        return Results.Ok(user);
    }
}

/// <summary>
/// Request DTO for logout operation.
/// </summary>
public sealed record LogoutRequest
{
    public required string RefreshToken { get; init; }
}
