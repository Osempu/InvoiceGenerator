namespace InvoiceGenerator.Core.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}