namespace InvoiceGenerator.Infrastructure.Authentication;

/// <summary>
/// Configuration settings for Keycloak identity provider integration.
/// </summary>
public sealed class KeycloakSettings
{
    public const string SectionName = "Keycloak";

    /// <summary>
    /// Base URL of the Keycloak server (e.g., http://localhost:8080).
    /// </summary>
    public string BaseUrl { get; init; } = string.Empty;

    /// <summary>
    /// Keycloak realm name.
    /// </summary>
    public string Realm { get; init; } = string.Empty;

    /// <summary>
    /// Client ID for the application.
    /// </summary>
    public string ClientId { get; init; } = string.Empty;

    /// <summary>
    /// Client secret for confidential clients.
    /// </summary>
    public string ClientSecret { get; init; } = string.Empty;

    /// <summary>
    /// OAuth2 authorization endpoint URL.
    /// </summary>
    public string AuthorizationUrl { get; init; } = string.Empty;

    /// <summary>
    /// OAuth2 token endpoint URL.
    /// </summary>
    public string TokenUrl { get; init; } = string.Empty;

    /// <summary>
    /// OpenID Connect userinfo endpoint URL.
    /// </summary>
    public string UserInfoUrl { get; init; } = string.Empty;

    /// <summary>
    /// OpenID Connect logout endpoint URL.
    /// </summary>
    public string LogoutUrl { get; init; } = string.Empty;

    /// <summary>
    /// Keycloak Admin REST API base URL for user management.
    /// </summary>
    public string AdminApiUrl { get; init; } = string.Empty;
}
