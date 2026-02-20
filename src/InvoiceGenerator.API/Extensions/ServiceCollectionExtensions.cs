using System.Diagnostics;
using Carter;
using InvoiceGenerator.API.CustomExceptions;
using InvoiceGenerator.API.Filters;
using InvoiceGenerator.Application.Configurations;
using InvoiceGenerator.Application.Services;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.Features;

namespace InvoiceGenerator.API.Extensions;

/// <summary>
/// Extension methods for configuring application services in the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the global exception handler for consistent error responses.
    /// </summary>
    public static IServiceCollection AddCustomErrorHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    /// <summary>
    /// Configures ProblemDetails with request tracing information.
    /// </summary>
    public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });

        return services;
    }

    /// <summary>
    /// Adds application-specific services (repositories, services, controllers).
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Repositories
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        // Application services
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }

    /// <summary>
    /// Adds Carter for minimal API endpoints and MVC controllers with audit logging.
    /// </summary>
    public static IServiceCollection AddApiEndpoints(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddControllers(options =>
        {
            options.Filters.Add<AuditLogFilter>();
        });

        return services;
    }

    /// <summary>
    /// Adds CORS with a policy that allows all origins (for development).
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="policyName">The name of the CORS policy.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddDevelopmentCors(
        this IServiceCollection services,
        string policyName = "AllowAll")
    {
        services.AddCors(options =>
        {
            options.AddPolicy(policyName, policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
        });

        return services;
    }
}
