using System.Text.Json.Serialization;
using InvoiceGenerator.Core.Model;
using InvoiceGenerator.Core.Responses.AddressResponses;

namespace InvoiceGenerator.Core.Responses;

public record CustomerResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public ICollection<AddressResponseDto>? Addresses { get; set; }
}
