using System;
using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Responses.AddressResponses;

public record AddressResponseDto
(
    int Id,
    string Street,
    string? Street2,
    string PostalCode,
    string State,
    string City,
    string Country,
    string AddressType,
    int CustomerId,
    Customer? Customer = null
);