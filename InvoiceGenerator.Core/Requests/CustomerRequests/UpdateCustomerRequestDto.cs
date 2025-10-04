using InvoiceGenerator.Core.Requests.AddressRequests;

namespace InvoiceGenerator.Core.Requests.CustomerRequests;

public record UpdateCustomerRequestDto
(
    int Id,
    string Name,
    string Description,
    ICollection<UpdateAddressRequestDto>? Address
);