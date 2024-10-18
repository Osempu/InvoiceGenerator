using InvoiceGenerator.Core.Model;

namespace InvoiceGenerator.Core.Models
{
    public class InvoiceDetails
    {
        public int Id { get; set; }
        public decimal SubTotal { get; set; }
        public int Tax { get;  set;}
        public decimal Total { get; set; }
        
        //Billing Address
        public int BillingAddressId { get; set; }
        public Address? BillingAddress { get; set; }

        //Line Items
        public ICollection<InvoiceLineItem>? InvoiceLineItems { get; set; }
        
        //Invoice
        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }
}