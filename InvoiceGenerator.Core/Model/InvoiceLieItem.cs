using System;
using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Model;

public class InvoiceLineItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => Item.Price * Quantity;

    //Item
    public int ItemId { get; set; }
    public required Item Item { get; set; }
    
    //Invoice Details
    public int InvoiceDetailsId { get; set; }
    public required InvoiceDetails InvoiceDetails { get; set; }
}
