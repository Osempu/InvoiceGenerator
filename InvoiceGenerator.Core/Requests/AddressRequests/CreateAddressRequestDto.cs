using System;

namespace InvoiceGenerator.Core.Requests.AddressRequests;

public record CreateAddressRequestDto
(
    string Street,
    string? Street2,
    string PostalCode,
    string State,
    string City,
    string Country,
    string AddressType,
    int CustomerId
);