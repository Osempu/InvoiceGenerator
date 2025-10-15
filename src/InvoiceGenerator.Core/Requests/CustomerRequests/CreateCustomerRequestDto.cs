using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Core.Requests.AddressRequests;

namespace InvoiceGenerator.Core.Requests;

public class CreateCustomerRequestDto
{
    public string? Name { get; init; }
    public string? Description { get; init; }

    public ICollection<CreateAddressRequestDto>? Addresses { get; init; }
}
