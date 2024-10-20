using System;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Core.Requests;
using InvoiceGenerator.Core.Requests.CustomerRequests;
using InvoiceGenerator.Core.Responses;

namespace InvoiceGenerator.Core.Contracts;

public interface ICustomerService
{
    public Task<IEnumerable<Customer>> GetAllCustomers();
    public Task<CustomerResponseDto> GetCustomerAsync(int id);
    public Task<Customer> CreateCustomer(CreateCustomerRequestDto customer);
    public Task<Customer> UpdateCustomer(int customerId, UpdateCustomerRequestDto customer);
    public Task DeleteCustomer(int id);
}
