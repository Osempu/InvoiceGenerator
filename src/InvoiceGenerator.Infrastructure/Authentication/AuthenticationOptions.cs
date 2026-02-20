namespace InvoiceGenerator.Infrastructure.Authentication;

/// <summary>
/// Configuration options for JWT Bearer authentication.
/// </summary>
public sealed class AuthenticationOptions
{
    public const string SectionName = "Authentication";

    /// <summary>
    /// The primary expected audience for the token.
    /// </summary>
    public string Audience { get; init; } = string.Empty;

    /// <summary>
    /// Additional valid audiences (for multi-client scenarios).
    /// </summary>
    public string[] ValidAudiences { get; init; } = [];

    /// <summary>
    /// The OpenID Connect metadata URL.
    /// </summary>
    public string MetadataUrl { get; init; } = string.Empty;

    /// <summary>
    /// Whether to require HTTPS for metadata retrieval.
    /// </summary>
    public bool RequireHttpsMetadata { get; init; }

    /// <summary>
    /// The expected token issuer.
    /// </summary>
    public string Issuer { get; init; } = string.Empty;

    /// <summary>
    /// Whether to validate the audience claim. Set to false during development if needed.
    /// </summary>
    public bool ValidateAudience { get; init; } = true;
}