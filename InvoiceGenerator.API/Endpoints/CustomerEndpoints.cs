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
        app.MapGet("/", async ([FromServices] ICustomerService customerService, [FromServices] ILogger<CustomersModule> logger) =>
        {
            var serviceResult = await customerService.GetAllCustomers();
            logger.LogInformation("Get All Customers");
            return Results.Ok(serviceResult.Value);
        });

        //Get Customer By Id
        app.MapGet("/{id}", async (ICustomerService customerService, int id) =>
        {
            var serviceResult = await customerService.GetCustomerAsync(id);

            return serviceResult.Match(
                    success: (customer) => Results.Ok(customer),
                    failure: (error) => serviceResult.ToProblemDetails());
        }).WithName("GetCustomerById");

        //Create Customer
        app.MapPost("/", async (ICustomerService customerService, [FromBody] CreateCustomerRequestDto customerDto) =>
        {
            var serviceResult = await customerService.CreateCustomer(customerDto);
            return serviceResult.Match(
                success: () => Results.CreatedAtRoute("GetCustomerById", new { id = serviceResult.Value.Id }, serviceResult.Value),
                failure: error => serviceResult.ToProblemDetails()
            );
        });

        //Update Customer
        app.MapPut("/{id}", async (ICustomerService customerService, int id, [FromBody] UpdateCustomerRequestDto customerDto) =>
        {
            var serviceResult = await customerService.UpdateCustomer(customerDto.Id, customerDto);

            serviceResult.Match(
                success: () => Results.NoContent(),
                failure: error => serviceResult.ToProblemDetails()
            );
        });

        //Delete Customer
        app.MapDelete("/{id}", async (ICustomerService customerService, int id, ILogger<CustomersModule> logger) =>
        {
            var serviceResult = await customerService.DeleteCustomer(id);

            return serviceResult.Match(
                        success: () => Results.NoContent(),
                        failure: error =>
                        {
                            logger.LogError("Error while deleting customer: {@error}", serviceResult.ToProblemDetails());
                            return serviceResult.ToProblemDetails();
                        });
        });
    }
}