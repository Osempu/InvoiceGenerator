using InvoiceGenerator.Infrastructure.Authentication;
using Microsoft.OpenApi.Models;

namespace InvoiceGenerator.API.Extensions;

/// <summary>
/// Extension methods for configuring Swagger/OpenAPI with OAuth2 authentication.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Adds Swagger generation with Keycloak OAuth2 security configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="keycloakSettings">The Keycloak settings for OAuth2 configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSwaggerWithOAuth(
        this IServiceCollection services,
        KeycloakSettings keycloakSettings)
    {
        ArgumentNullException.ThrowIfNull(keycloakSettings);

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

            options.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(keycloakSettings.AuthorizationUrl),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID Connect" },
                            { "profile", "User profile" },
                            { "email", "User email" }
                        }
                    }
                }
            });

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Keycloak",
                            Type = ReferenceType.SecurityScheme
                        },
                        In = ParameterLocation.Header,
                        Name = "Bearer",
                        Scheme = "Bearer"
                    },
                    []
                }
            };

            options.AddSecurityRequirement(securityRequirement);

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Invoice Generator API",
                Version = "v1",
                Description = "An API for managing invoices, customers, and items"
            });
        });

        return services;
    }

    /// <summary>
    /// Configures Swagger UI middleware with Keycloak OAuth2 settings.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <param name="keycloakSettings">The Keycloak settings for OAuth2 configuration.</param>
    /// <returns>The web application for chaining.</returns>
    public static WebApplication UseSwaggerWithOAuth(
        this WebApplication app,
        KeycloakSettings keycloakSettings)
    {
        ArgumentNullException.ThrowIfNull(keycloakSettings);

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice Generator API v1");
            options.OAuthClientId(keycloakSettings.ClientId);
            options.OAuthScopes("openid", "profile", "email");
            options.OAuthUsePkce();
        });

        return app;
    }
}
