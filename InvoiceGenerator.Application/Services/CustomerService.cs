using System;
using InvoiceGenerator.Application.Configurations;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Mappings;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Core.Requests;
using InvoiceGenerator.Core.Requests.CustomerRequests;
using InvoiceGenerator.Core.Responses;

namespace InvoiceGenerator.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository customerRepository;
    public CustomerService(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public async Task<Customer> CreateCustomer(CreateCustomerRequestDto customerDto)
    {
        if(customerDto is null) {
            throw new ArgumentNullException(nameof(Customer));
        }

        var customer = customerDto.MapToCustomer();
        var newCustomer =  await customerRepository.CreateCustomerAsync(customer);

        if(newCustomer is null) {
            throw new NullReferenceException($"Customer with Id: {customer.Id} was not created");
        }

        return newCustomer;
    }

    public async Task DeleteCustomer(int id)
    {
        var customer = await customerRepository.GetCustomerAsync(id);

        if(customer is null){
            throw new NullReferenceException($"Customer with Id: {id} was not found");
        }

        await customerRepository.DeleteCustomerAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await customerRepository.GetAllCustomersAsync();
    }

    public async Task<CustomerResponseDto> GetCustomerAsync(int id)
    {
        var customer =  await customerRepository.GetCustomerAsync(id)
            ?? throw new DllNotFoundException("Customer with Id was not found");

        return customer.MapToCustomerResponse();
    }

    public async Task<Customer> UpdateCustomer(int customerId, UpdateCustomerRequestDto customerDto)
    {
        var customer = await customerRepository.GetCustomerAsync(customerId, trackChanges: true);

        if(customer is null) {
            throw new NullReferenceException($"Customer with Id: {customerId} was not found");
        }

        customerDto.MapToCustomer(customer);
        return await customerRepository.UpdateCustomerASync(customer);
    }
}
