using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InvoiceGenerator.Infrastructure.Authentication;

/// <summary>
/// Configures JWT Bearer authentication options using values from AuthenticationOptions.
/// </summary>
public sealed class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly AuthenticationOptions _authenticationOptions;

    public JwtBearerOptionsSetup(IOptions<AuthenticationOptions> authenticationOptions)
    {
        _authenticationOptions = authenticationOptions.Value;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }

    public void Configure(JwtBearerOptions options)
    {
        options.MetadataAddress = _authenticationOptions.MetadataUrl;
        options.RequireHttpsMetadata = _authenticationOptions.RequireHttpsMetadata;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _authenticationOptions.Issuer,

            ValidateAudience = _authenticationOptions.ValidateAudience,
            ValidAudience = _authenticationOptions.Audience,
            ValidAudiences = _authenticationOptions.ValidAudiences.Length > 0
                ? _authenticationOptions.ValidAudiences
                : null,

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            // Keycloak uses 'sub' claim for name identifier
            NameClaimType = "preferred_username",
            RoleClaimType = "roles"
        };

        // For development: add events to help debug token issues
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices
                    .GetService<ILoggerFactory>()?
                    .CreateLogger<JwtBearerOptionsSetup>();

                logger?.LogWarning(
                    context.Exception,
                    "Authentication failed: {Message}",
                    context.Exception.Message);

                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var logger = context.HttpContext.RequestServices
                    .GetService<ILoggerFactory>()?
                    .CreateLogger<JwtBearerOptionsSetup>();

                logger?.LogDebug(
                    "Token validated for user: {User}",
                    context.Principal?.Identity?.Name);

                return Task.CompletedTask;
            }
        };
    }
}