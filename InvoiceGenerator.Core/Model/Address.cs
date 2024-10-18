using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Model;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string? Street2 { get; set; }
    public string PostalCode { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string AddressType { get; set; }

    //Customer
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    //Invoice Details Collection
    public ICollection<InvoiceDetails>? InvoiceDetails { get; set; }

    public Address(
        int id, 
        string street, 
        string postalCode, 
        string state, 
        string city, 
        string country, 
        string addressType,
        int customerId,
        string street2 = "")
    {
        Id = id;
        Street = street;
        Street2 = !string.IsNullOrWhiteSpace(street2) ? street2 : null;
        PostalCode = postalCode;
        State = state;
        City = city;
        Country = country;
        AddressType = addressType;
        CustomerId = customerId;
    }
}
