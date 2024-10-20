using InvoiceGenerator.Core.Requests.CustomerRequests;
using InvoiceGenerator.Core.Requests.AddressRequests;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Requests;
using InvoiceGenerator.Core.Mappings;
using Carter;

namespace InvoiceGenerator.API.Endpoints;

public class CustomersModule : CarterModule
{
    public CustomersModule() :base("api/customers")
    {
        WithTags("Customers");
        IncludeInOpenApi();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        //Get All Customers
        app.MapGet("/customers", async (ICustomerService customerService) => {
            var customers = await customerService.GetAllCustomers();
            return Results.Ok(customers);
        }).WithOpenApi();

        //Get Customer By Id
        app.MapGet("/customers/{id}", async (ICustomerService customerService, int id) => {
            var customer = await customerService.GetCustomerAsync(id);
            return Results.Ok(customer);
        }).WithOpenApi().WithName("GetCustomerById");

        //Create Customer
        app.MapPost("/customers", async (ICustomerService customerService, CreateCustomerRequestDto customerDto) => {
            var newCustomer = await customerService.CreateCustomer(customerDto);
            return Results.CreatedAtRoute("GetCustomerById", new {id = newCustomer.Id}, newCustomer);
        }).WithOpenApi();

        //Update Customer
        app.MapPut("/customers/{id}", async (ICustomerService customerService,int customerId, UpdateCustomerRequestDto customerDto) => {
            var updatedCustomer = await customerService.UpdateCustomer(customerDto.Id, customerDto);
            return Results.NoContent();
        });

        //Delete Customer
        app.MapDelete("/customer/{id}", async (ICustomerService customerService, int customerId) => {
            await customerService.DeleteCustomer(customerId);
            return Results.NoContent();
        });
    }
}
