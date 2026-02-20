using InvoiceGenerator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.API.Extensions;

/// <summary>
/// Extension methods for configuring database and Entity Framework Core services.
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Adds and configures the application database context with PostgreSQL.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="isDevelopment">Whether the application is running in a development environment.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment = false)
    {
        var connectionString = configuration.GetConnectionString("SBDevConnection")
            ?? throw new InvalidOperationException("Database connection string 'SBDevConnection' is not configured.");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);

            if (isDevelopment)
            {
                options.LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name])
                       .EnableSensitiveDataLogging();
            }
        });

        return services;
    }

    /// <summary>
    /// Applies any pending database migrations at application startup.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>The web application for chaining.</returns>
    public static WebApplication UseDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();

        return app;
    }
}
