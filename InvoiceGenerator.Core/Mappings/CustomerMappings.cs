using InvoiceGenerator.Core.Model;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Core.Requests;
using InvoiceGenerator.Core.Requests.CustomerRequests;
using InvoiceGenerator.Core.Responses;
using InvoiceGenerator.Core.Responses.AddressResponses;

namespace InvoiceGenerator.Core.Mappings;

public static class CustomerMappings
{
    //CreateCustomerRequestDTO => Customer
    public static Customer MapToCustomer(this CreateCustomerRequestDto customerRequest)
    {
        return new Customer {
            Id = customerRequest.Id,
            Name = customerRequest.Name,
            Description = customerRequest.Description,
            Addresses = customerRequest.Addresses?
                .Select(x => x.MapToAddress()).ToList() ?? Enumerable.Empty<Address>().ToList()
        };
    }

    //Customer => CustomerResponse
    public static CustomerResponseDto MapToCustomerResponse(this Customer customer)
    {
        return new CustomerResponseDto {
            Id = customer.Id,
            Name = customer.Name,
            Description = customer.Description,
            AddressesResponseDto = customer.Addresses?
                .Select(x => x.MapToAddressResponseDto()).ToList() ?? Enumerable.Empty<AddressResponseDto>().ToList()
        };
    }

    //UpdateCustomerRequestDTO => Fill Customer
    public static Customer MapToCustomer(this UpdateCustomerRequestDto customerRequest, Customer customer)
    {
        customer.Id = customerRequest.Id;
        customer.Name = customerRequest.Name;
        customer.Description = customerRequest.Description;
        customer.Addresses?.Clear();
        customerRequest.Address?.ToList().ForEach(x => customer.Addresses?.Add(x.MapToAddress()));
        return customer;
    }
}
