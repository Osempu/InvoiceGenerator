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
builder.Services.AddSwaggerGen();
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
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseGlobalErrorHandling();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapCarter();
app.Run();