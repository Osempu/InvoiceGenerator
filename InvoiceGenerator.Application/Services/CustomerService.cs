using InvoiceGenerator.Core.Requests.CustomerRequests;
using InvoiceGenerator.Application.Configurations;
using InvoiceGenerator.Core.Responses.ResultType;
using InvoiceGenerator.Core.Validators.Customer;
using InvoiceGenerator.Core.Responses.Errors;
using InvoiceGenerator.Core.Responses;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Mappings;
using InvoiceGenerator.Core.Requests;
using Microsoft.Extensions.Logging;

namespace InvoiceGenerator.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository customerRepository;
    public ILogger<CustomerService> logger { get; }

    public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
    {
        this.logger = logger;
        this.customerRepository = customerRepository;
    }

    public async Task<Result<IEnumerable<CustomerResponseDto>>> GetAllCustomers()
    {
        var customers = await customerRepository.GetAllCustomersAsync();
        var customersResponse = customers.Select(x => x.MapToCustomerResponse());
        return Result<IEnumerable<CustomerResponseDto>>.Success(customersResponse);
    }

    public async Task<Result<CustomerResponseDto>> GetCustomerAsync(int id)
    {
        var customer = await customerRepository.GetCustomerAsync(id);

        if (customer is null)
        {
            logger.LogError("Customer with id: {Id} does not exist in database", id);
            return Error.NotFound<CustomerResponseDto>(id);
        }

        return Result<CustomerResponseDto>.Success(customer.MapToCustomerResponse());
    }

    public async Task<Result<CustomerResponseDto>> CreateCustomer(CreateCustomerRequestDto customerDto)
    {
        var validationResult = await new CreateCustomerValidator().ValidateAsync(customerDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => new Error(x.ErrorCode, x.ErrorMessage, ErrorType.Validation));
            return Error.ValidationError<CreateCustomerRequestDto>(errors);
        }

        var customer = customerDto.MapToCustomer();
        var newCustomer = await customerRepository.CreateCustomerAsync(customer);

        if (newCustomer is null)
        {
            return new Error("Create.Error", "Customer was not created", ErrorType.Failure);
        }

        return newCustomer.MapToCustomerResponse();
    }

    public async Task<Result<CustomerResponseDto>> UpdateCustomer(int customerId, UpdateCustomerRequestDto customerDto)
    {
        var customer = await customerRepository.GetCustomerAsync(customerId, trackChanges: true);

        if (customer is null)
        {
            return Error.NotFound<CustomerResponseDto>(customerId);
        }

        customerDto.MapToCustomer(customer);
        var updatedCustomer = await customerRepository.UpdateCustomerAsync(customer);

        if (updatedCustomer is null)
        {
            return Error.NotFound<CustomerResponseDto>(customerId);
        }

        return updatedCustomer.MapToCustomerResponse();
    }


    public async Task<Result> DeleteCustomer(int id)
    {
        var customer = await customerRepository.GetCustomerAsync(id);

        if (customer is null)
        {
            return Result.Failure(Error.NotFound<CustomerResponseDto>(id));
        }

        await customerRepository.DeleteCustomerAsync(id);
        return Result.Success();
    }
}
