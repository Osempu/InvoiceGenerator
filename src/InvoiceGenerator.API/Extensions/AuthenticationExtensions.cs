using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InvoiceGenerator.API.Extensions;

/// <summary>
/// Extension methods for configuring authentication and authorization services.
/// </summary>
public static class AuthenticationExtensions
{
    /// <summary>
    /// Adds Keycloak authentication and authorization services to the application.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddKeycloakAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Bind Keycloak and Authentication options from configuration
        services.Configure<KeycloakSettings>(
            configuration.GetSection(KeycloakSettings.SectionName));
        services.Configure<AuthenticationOptions>(
            configuration.GetSection(AuthenticationOptions.SectionName));

        // Register HTTP context accessor for accessing current user claims
        services.AddHttpContextAccessor();

        // Register identity services
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpClient<IIdentityService, KeycloakIdentityService>();

        // Configure JWT Bearer authentication with options from JwtBearerOptionsSetup
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        // Add authorization
        services.AddAuthorizationBuilder();

        return services;
    }
}
