using System;
using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Model;

public class InvoiceLineItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => (Item?.Price ?? 0) * Quantity;

    //Item
    public int ItemId { get; set; }
    public Item? Item { get; set; }

    //Invoice Details
    public int InvoiceDetailsId { get; set; }
    public InvoiceDetails? InvoiceDetails { get; set; }
}
