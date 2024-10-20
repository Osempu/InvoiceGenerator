using System;
using InvoiceGenerator.Core.Model;
using InvoiceGenerator.Core.Requests.AddressRequests;
using InvoiceGenerator.Core.Responses.AddressResponses;

namespace InvoiceGenerator.Core.Mappings;

public static class AddressMappings
{
    public static Address MapToAddress(this CreateAddressRequestDto addressRequest)
    {
        return new Address
        {
            Street = addressRequest.Street,
            Street2 = !string.IsNullOrWhiteSpace(addressRequest.Street2) ? addressRequest.Street2 : null,
            PostalCode = addressRequest.PostalCode,
            State = addressRequest.State,
            City = addressRequest.City,
            Country = addressRequest.Country,
            AddressType = addressRequest.AddressType,
            CustomerId = addressRequest.CustomerId
        };
    }

    //Address => AddressResponse
    public static AddressResponseDto MapToAddressResponseDto(this Address address)
    {
        return new AddressResponseDto(
            address.Id,
            address.Street,
            address.Street2,
            address.PostalCode,
            address.State,
            address.City,
            address.Country,
            address.AddressType,
            address.CustomerId
        );
    }

    //UpdateAddressRequestDto => Fill Address
    public static Address MapToAddress(this UpdateAddressRequestDto addressRequest)
    {
        return new Address
        {
            Id = addressRequest.Id,
            Street = addressRequest.Street,
            Street2 = !string.IsNullOrWhiteSpace(addressRequest.Street2) ? addressRequest.Street2 : null,
            PostalCode = addressRequest.PostalCode,
            State = addressRequest.State,
            City = addressRequest.City,
            Country = addressRequest.Country,
            AddressType = addressRequest.AddressType,
            CustomerId = addressRequest.CustomerId
        };
    }
}
