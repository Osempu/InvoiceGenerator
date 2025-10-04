using InvoiceGenerator.Core.Requests.CustomerRequests;
using InvoiceGenerator.Core.Responses.ResultType;
using InvoiceGenerator.API.Extensions;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Requests;
using Microsoft.AspNetCore.Mvc;
using Carter;

namespace InvoiceGenerator.API.Endpoints;

public class CustomersModule : CarterModule
{
    public CustomersModule() : base("api/customers")
    {
        WithTags("Customers");
        IncludeInOpenApi();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        //Get All Customers
        app.MapGet("/", async ([FromServices] ICustomerService customerService, [FromServices] ILogger<CustomersModule> logger, HttpContext httpContext) =>
        {
            using var activity = logger.BeginScope("CorrelationId: {CorrelationId}, Operation: {Operation}",
                httpContext.TraceIdentifier, "GetAllCustomers");
            logger.LogInformation("Retrieving all customers");

            var serviceResult = await customerService.GetAllCustomers();

            if (serviceResult.IsSuccess)
            {
                logger.LogInformation("Successfully retrieved {CustomerCount} customers", serviceResult.Value?.Count() ?? 0);
                return Results.Ok(serviceResult.Value);
            }

            logger.LogWarning("Failed to retrieve customers: {Error}", serviceResult.Error);
            return serviceResult.ToProblemDetails();
        });

        //Get Customer By Id
        app.MapGet("/{id}", async (ICustomerService customerService, int id, ILogger<CustomersModule> logger, HttpContext httpContext) =>
        {
            using var activity = logger.BeginScope("CorrelationId: {CorrelationId}, Operation: {Operation}, CustomerId: {CustomerId}",
                httpContext.TraceIdentifier, "GetCustomerById", id);
            logger.LogInformation("Retrieving customer with ID: {CustomerId}", id);

            var serviceResult = await customerService.GetCustomerAsync(id);

            return serviceResult.Match(
                success: (customer) =>
                {
                    logger.LogInformation("Successfully retrieved customer {CustomerId} - {CustomerName}", id, customer.Name);
                    return Results.Ok(customer);
                },
                failure: (error) =>
                {
                    logger.LogWarning("Failed to retrieve customer {CustomerId}: {Error}", id, error);
                    return serviceResult.ToProblemDetails();
                });
        }).WithName("GetCustomerById");

        //Create Customer
        app.MapPost("/", async (ICustomerService customerService, [FromBody] CreateCustomerRequestDto customerDto, ILogger<CustomersModule> logger, HttpContext httpContext) =>
        {
            using var activity = logger.BeginScope("CorrelationId: {CorrelationId}, Operation: {Operation}",
                httpContext.TraceIdentifier, "CreateCustomer");
            logger.LogInformation("Creating new customer: {CustomerName}", customerDto.Name);

            var serviceResult = await customerService.CreateCustomer(customerDto);
            return serviceResult.Match(
                success: () =>
                {
                    logger.LogInformation("Successfully created customer {CustomerId} - {CustomerName}", serviceResult.Value.Id, serviceResult.Value.Name);
                    return Results.CreatedAtRoute("GetCustomerById", new { id = serviceResult.Value.Id }, serviceResult.Value);
                },
                failure: error =>
                {
                    logger.LogError("Failed to create customer {CustomerName}: {Error}", customerDto.Name, error);
                    return serviceResult.ToProblemDetails();
                }
            );
        });

        //Update Customer
        app.MapPut("/{id}", async (ICustomerService customerService, int id, [FromBody] UpdateCustomerRequestDto customerDto, ILogger<CustomersModule> logger, HttpContext httpContext) =>
        {
            using var activity = logger.BeginScope("CorrelationId: {CorrelationId}, Operation: {Operation}, CustomerId: {CustomerId}",
                httpContext.TraceIdentifier, "UpdateCustomer", id);
            logger.LogInformation("Updating customer {CustomerId} with new data: {CustomerName}", id, customerDto.Name);

            var serviceResult = await customerService.UpdateCustomer(id, customerDto);

            return serviceResult.Match(
                success: () =>
                {
                    logger.LogInformation("Successfully updated customer {CustomerId}", id);
                    return Results.NoContent();
                },
                failure: error =>
                {
                    logger.LogError("Failed to update customer {CustomerId}: {Error}", id, error);
                    return serviceResult.ToProblemDetails();
                }
            );
        });

        //Delete Customer
        app.MapDelete("/{id}", async (ICustomerService customerService, int id, ILogger<CustomersModule> logger, HttpContext httpContext) =>
        {
            using var activity = logger.BeginScope("CorrelationId: {CorrelationId}, Operation: {Operation}, CustomerId: {CustomerId}",
                httpContext.TraceIdentifier, "DeleteCustomer", id);
            logger.LogInformation("Attempting to delete customer {CustomerId}", id);

            var serviceResult = await customerService.DeleteCustomer(id);

            return serviceResult.Match(
                success: () =>
                {
                    logger.LogInformation("Successfully deleted customer {CustomerId}", id);
                    return Results.NoContent();
                },
                failure: error =>
                {
                    logger.LogError("Failed to delete customer {CustomerId}: {Error}", id, error);
                    return serviceResult.ToProblemDetails();
                });
        });
    }
}