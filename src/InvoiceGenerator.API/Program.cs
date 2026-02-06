using InvoiceGenerator.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InvoiceGenerator.Infrastructure.Repositories;
using InvoiceGenerator.Application.Configurations;
using InvoiceGenerator.Application.Services;
using InvoiceGenerator.Infrastructure.Data;
using InvoiceGenerator.Application.Options;
using InvoiceGenerator.API.Controllers;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.API.Extensions;
using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.API.Filters;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Serilog;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddOptions<CustomSettings>()
    .Bind(builder.Configuration.GetSection("CustomSettings"))
    .ValidateDataAnnotations();

builder.Services.AddEndpointsApiExplorer();

//Move into a separate method later
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

    options.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(builder.Configuration["Keycloak:AuthorizationUrl"]!),
                Scopes = new Dictionary<string, string>
                {
                   {"openid", "openid"},
                   {"profile", "profile"}
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

    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Invoice Generator API",
        Version = "v1",
        Description = "An API for managing invoices, customers, and items"
    });
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<InvoiceController>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("SBDevConnection"))
           .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name })
           .EnableSensitiveDataLogging();
});
builder.Services.AddCustomErrorHandling();
builder.Services.ConfigureProblemDetails();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection("Authentication"));

builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddCarter();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<AuditLogFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice Generator API v1");
    });
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

app.MapGet("users/me", (ClaimsPrincipal claimsPrincipal) =>
{
    return claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value);
}).RequireAuthorization();


app.MapCarter();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
