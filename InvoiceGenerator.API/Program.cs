using Carter;
using InvoiceGenerator.Application.Configurations;
using InvoiceGenerator.Application.Services;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Infrastructure.Data;
using InvoiceGenerator.Infrastructure.Data.Extensions;
using InvoiceGenerator.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddDbContext<AppDbContext>();


builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();
app.Run();