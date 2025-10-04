using InvoiceGenerator.Infrastructure.Repositories;
using InvoiceGenerator.Application.Configurations;
using InvoiceGenerator.Application.Services;
using InvoiceGenerator.Infrastructure.Data;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.API.Extensions;
using Serilog;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Invoice Generator API",
        Version = "v1",
        Description = "An API for managing invoices, customers, and items"
    });
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddCustomErrorHandling();
builder.Services.ConfigureProblemDetails();
builder.Services.AddCarter();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice Generator API v1");
    });
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
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapCarter();
app.Run();