using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Model;

public class Address(
    string street,
    string postalCode,
    string state,
    string city,
    string country,
    string addressType,
    string? street2 = null)
{
    public int Id { get; set; }
    public string Street { get; set; } = street;
    public string? Street2 { get; set; } = !string.IsNullOrWhiteSpace(street2) ? street2 : null;
    public string PostalCode { get; set; } = postalCode;
    public string State { get; set; } = state;
    public string City { get; set; } = city;
    public string Country { get; set; } = country;
    public string AddressType { get; set; } = addressType;

    //Customer
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
