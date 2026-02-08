using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Requests.AuthRequests;
using InvoiceGenerator.Core.Responses;
using InvoiceGenerator.Core.Responses.AuthResponses;
using InvoiceGenerator.Core.Responses.Errors;
using InvoiceGenerator.Core.Responses.ResultType;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace InvoiceGenerator.Infrastructure.Authentication;

/// <summary>
/// Keycloak implementation of the identity service for user authentication and management.
/// Works with public clients (no client secret required for token operations).
/// </summary>
public sealed class KeycloakIdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakSettings _settings;
    private readonly ILogger<KeycloakIdentityService> _logger;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public KeycloakIdentityService(
        HttpClient httpClient,
        IOptions<KeycloakSettings> settings,
        ILogger<KeycloakIdentityService> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Result<string>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        using var scope = _logger.BeginScope("Registering user {Email}", request.Email);

        try
        {
            // For public clients, we use the Admin API with service account credentials
            // If ClientSecret is empty, we need to use a separate admin client or self-registration
            if (string.IsNullOrEmpty(_settings.ClientSecret))
            {
                _logger.LogWarning("Client secret not configured. User registration via Admin API requires a confidential client.");
                return Result<string>.Fail(Error.Failure("User.RegistrationNotConfigured", 
                    "User registration is not configured. Please use Keycloak's self-registration or configure a confidential client."));
            }

            // Get an admin token using client credentials grant
            var adminToken = await GetAdminTokenAsync(cancellationToken);
            if (adminToken.IsFailure)
            {
                return Result<string>.Fail(adminToken.Error!);
            }

            // Create the user in Keycloak
            var keycloakUser = new KeycloakUserRepresentation
            {
                Username = request.Username ?? request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Enabled = true,
                EmailVerified = false,
                Credentials =
                [
                    new KeycloakCredential
                    {
                        Type = "password",
                        Value = request.Password,
                        Temporary = false
                    }
                ]
            };

            using var createRequest = new HttpRequestMessage(HttpMethod.Post, $"{_settings.AdminApiUrl}/users");
            createRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", adminToken.Value);
            createRequest.Content = JsonContent.Create(keycloakUser, options: JsonOptions);

            using var response = await _httpClient.SendAsync(createRequest, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                _logger.LogWarning("User registration failed: user {Email} already exists", request.Email);
                return Result<string>.Fail(Error.Conflict("User.AlreadyExists", $"A user with email '{request.Email}' already exists."));
            }

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("User registration failed with status {StatusCode}: {Error}", response.StatusCode, errorContent);
                return Result<string>.Fail(Error.Failure("User.RegistrationFailed", "Failed to register user."));
            }

            // Extract user ID from Location header
            var locationHeader = response.Headers.Location?.ToString();
            var userId = locationHeader?.Split('/').LastOrDefault() ?? string.Empty;

            _logger.LogInformation("User {Email} registered successfully with ID {UserId}", request.Email, userId);
            return Result<string>.Success(userId);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error during user registration for {Email}", request.Email);
            return Result<string>.Fail(Error.Failure("User.RegistrationFailed", "Failed to connect to identity provider."));
        }
    }

    /// <inheritdoc />
    public async Task<Result<TokenResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        using var scope = _logger.BeginScope("Authenticating user {Username}", request.Username);

        try
        {
            // Build token request - for public clients, don't include client_secret
            var tokenParams = new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["client_id"] = _settings.ClientId,
                ["username"] = request.Username,
                ["password"] = request.Password,
                ["scope"] = "openid profile email"
            };

            // Only include client_secret if configured (confidential client)
            if (!string.IsNullOrEmpty(_settings.ClientSecret))
            {
                tokenParams["client_secret"] = _settings.ClientSecret;
            }

            var tokenRequest = new FormUrlEncodedContent(tokenParams);

            using var response = await _httpClient.PostAsync(_settings.TokenUrl, tokenRequest, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogWarning("Login failed for user {Username}: {Error}", request.Username, errorContent);
                return Result<TokenResponse>.Fail(Error.Unauthorized("Auth.InvalidCredentials", "Invalid username or password."));
            }

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Login failed with status {StatusCode}: {Error}", response.StatusCode, errorContent);
                return Result<TokenResponse>.Fail(Error.Failure("Auth.LoginFailed", "Authentication failed."));
            }

            var keycloakResponse = await response.Content.ReadFromJsonAsync<KeycloakTokenResponse>(JsonOptions, cancellationToken);
            if (keycloakResponse is null)
            {
                return Result<TokenResponse>.Fail(Error.Failure("Auth.LoginFailed", "Invalid response from identity provider."));
            }

            var tokenResponse = new TokenResponse
            {
                AccessToken = keycloakResponse.AccessToken,
                RefreshToken = keycloakResponse.RefreshToken,
                ExpiresIn = keycloakResponse.ExpiresIn,
                RefreshExpiresIn = keycloakResponse.RefreshExpiresIn,
                TokenType = keycloakResponse.TokenType
            };

            _logger.LogInformation("User {Username} authenticated successfully", request.Username);
            return Result<TokenResponse>.Success(tokenResponse);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error during login for {Username}", request.Username);
            return Result<TokenResponse>.Fail(Error.Failure("Auth.LoginFailed", "Failed to connect to identity provider."));
        }
    }

    /// <inheritdoc />
    public async Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Refreshing access token");

        try
        {
            // Build token request - for public clients, don't include client_secret
            var tokenParams = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = _settings.ClientId,
                ["refresh_token"] = request.RefreshToken
            };

            // Only include client_secret if configured (confidential client)
            if (!string.IsNullOrEmpty(_settings.ClientSecret))
            {
                tokenParams["client_secret"] = _settings.ClientSecret;
            }

            var tokenRequest = new FormUrlEncodedContent(tokenParams);

            using var response = await _httpClient.PostAsync(_settings.TokenUrl, tokenRequest, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest ||
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _logger.LogWarning("Token refresh failed: invalid or expired refresh token");
                return Result<TokenResponse>.Fail(Error.Unauthorized("Auth.InvalidRefreshToken", "Refresh token is invalid or expired."));
            }

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Token refresh failed with status {StatusCode}: {Error}", response.StatusCode, errorContent);
                return Result<TokenResponse>.Fail(Error.Failure("Auth.RefreshFailed", "Failed to refresh token."));
            }

            var keycloakResponse = await response.Content.ReadFromJsonAsync<KeycloakTokenResponse>(JsonOptions, cancellationToken);
            if (keycloakResponse is null)
            {
                return Result<TokenResponse>.Fail(Error.Failure("Auth.RefreshFailed", "Invalid response from identity provider."));
            }

            var tokenResponse = new TokenResponse
            {
                AccessToken = keycloakResponse.AccessToken,
                RefreshToken = keycloakResponse.RefreshToken,
                ExpiresIn = keycloakResponse.ExpiresIn,
                RefreshExpiresIn = keycloakResponse.RefreshExpiresIn,
                TokenType = keycloakResponse.TokenType
            };

            _logger.LogDebug("Token refreshed successfully");
            return Result<TokenResponse>.Success(tokenResponse);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error during token refresh");
            return Result<TokenResponse>.Fail(Error.Failure("Auth.RefreshFailed", "Failed to connect to identity provider."));
        }
    }

    /// <inheritdoc />
    public async Task<Result> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Logging out user");

        try
        {
            // Build logout request - for public clients, don't include client_secret
            var logoutParams = new Dictionary<string, string>
            {
                ["client_id"] = _settings.ClientId,
                ["refresh_token"] = refreshToken
            };

            // Only include client_secret if configured (confidential client)
            if (!string.IsNullOrEmpty(_settings.ClientSecret))
            {
                logoutParams["client_secret"] = _settings.ClientSecret;
            }

            var logoutRequest = new FormUrlEncodedContent(logoutParams);

            using var response = await _httpClient.PostAsync(_settings.LogoutUrl, logoutRequest, cancellationToken);

            // Keycloak returns 204 No Content on successful logout
            if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogWarning("Logout request returned status {StatusCode}: {Error}", response.StatusCode, errorContent);
                // We still consider this a success from the client's perspective
            }

            _logger.LogInformation("User logged out successfully");
            return Result.Success();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error during logout");
            return Result.Failure(Error.Failure("Auth.LogoutFailed", "Failed to connect to identity provider."));
        }
    }

    private async Task<Result<string>> GetAdminTokenAsync(CancellationToken cancellationToken)
    {
        // For user registration via Admin API, we need client credentials grant
        // This requires a confidential client with service account enabled
        if (string.IsNullOrEmpty(_settings.ClientSecret))
        {
            _logger.LogError("Cannot obtain admin token: client secret not configured");
            return Result<string>.Fail(Error.Failure("Auth.AdminTokenFailed", 
                "Admin operations require a confidential client with service account enabled."));
        }

        var tokenRequest = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "client_credentials",
            ["client_id"] = _settings.ClientId,
            ["client_secret"] = _settings.ClientSecret
        });

        using var response = await _httpClient.PostAsync(_settings.TokenUrl, tokenRequest, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Failed to obtain admin token: {StatusCode} - {Error}", response.StatusCode, errorContent);
            return Result<string>.Fail(Error.Failure("Auth.AdminTokenFailed", 
                "Failed to obtain administrative access. Ensure the client has service accounts enabled and has the 'manage-users' role."));
        }

        var tokenResponse = await response.Content.ReadFromJsonAsync<KeycloakTokenResponse>(JsonOptions, cancellationToken);
        return tokenResponse?.AccessToken is null
            ? Result<string>.Fail(Error.Failure("Auth.AdminTokenFailed", "Invalid token response."))
            : Result<string>.Success(tokenResponse.AccessToken);
    }

    #region Keycloak DTOs

    private sealed class KeycloakTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_expires_in")]
        public int RefreshExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;
    }

    private sealed class KeycloakUserRepresentation
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("emailVerified")]
        public bool EmailVerified { get; set; }

        [JsonPropertyName("credentials")]
        public List<KeycloakCredential> Credentials { get; set; } = [];
    }

    private sealed class KeycloakCredential
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;

        [JsonPropertyName("temporary")]
        public bool Temporary { get; set; }
    }

    #endregion
}
