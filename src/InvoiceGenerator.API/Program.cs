using Carter;
using InvoiceGenerator.Infrastructure.Authentication;
using InvoiceGenerator.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


var keycloakSettings = builder.Configuration
    .GetSection(KeycloakSettings.SectionName)
    .Get<KeycloakSettings>()
    ?? throw new InvalidOperationException("Keycloak settings are not configured.");


var isDevelopmentOrDocker = builder.Environment.IsDevelopment()
    || builder.Environment.EnvironmentName.Equals("Docker", StringComparison.OrdinalIgnoreCase);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddSwaggerWithOAuth(keycloakSettings)
    .AddKeycloakAuthentication(builder.Configuration)
    .AddDatabase(builder.Configuration, isDevelopment: isDevelopmentOrDocker)
    .AddCustomErrorHandling()
    .ConfigureProblemDetails()
    .AddApiEndpoints()
    .AddDevelopmentCors();


var app = builder.Build();

app.UseDatabaseMigrations();

if (isDevelopmentOrDocker)
{
    app.UseSwaggerWithOAuth(keycloakSettings);
    app.UseDeveloperExceptionPage();
}

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value!);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
    };
});

app.UseGlobalErrorHandling();

// Only use HTTPS redirection when not in Docker (Docker uses HTTP only)
if (!app.Environment.EnvironmentName.Equals("Docker", StringComparison.OrdinalIgnoreCase))
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();
app.MapControllers();

await app.StartAsync();

if (app.Environment.IsDevelopment())
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    foreach (var url in app.Urls)
    {
        logger.LogInformation("Now listening on: {Url}", url);
    }
    logger.LogInformation("Application is ready.");
}

await app.WaitForShutdownAsync();
