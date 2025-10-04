using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Core.Requests;
using InvoiceGenerator.Core.Requests.CustomerRequests;
using InvoiceGenerator.Core.Responses;
using InvoiceGenerator.Core.Responses.ResultType;

namespace InvoiceGenerator.Core.Contracts;

public interface ICustomerService
{
    public Task<Result<IEnumerable<CustomerResponseDto>>> GetAllCustomers();
    public Task<Result<CustomerResponseDto>> GetCustomerAsync(int id);
    public Task<Result<CustomerResponseDto>> CreateCustomer(CreateCustomerRequestDto customer);
    public Task<Result<CustomerResponseDto>> UpdateCustomer(int customerId, UpdateCustomerRequestDto customer);
    public Task<Result> DeleteCustomer(int id);
}
