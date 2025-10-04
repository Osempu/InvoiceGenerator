using System;
using InvoiceGenerator.Core.Model;
using InvoiceGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Data.Seeding;

public static class InvoiceSeeding
{
    public static void InvoiceSeed(this ModelBuilder builder)
    {
        builder.Entity<Invoice>().HasData(
            new Invoice {Id = 1, Identifier = "INV-467958", ValidStartDate = new DateOnly(2024, 10, 19).AddDays(5), DueDate = new DateOnly(2024, 10, 19).AddDays(30), CustomerId = 1},
            new Invoice {Id = 2, Identifier = "INV-134678", ValidStartDate = new DateOnly(2024, 10, 17).AddDays(5), DueDate = new DateOnly(2024, 10, 17).AddDays(30), CustomerId = 1},
            new Invoice {Id = 3, Identifier = "INV-258795", ValidStartDate = new DateOnly(2024, 10, 15).AddDays(5), DueDate = new DateOnly(2024, 10, 15).AddDays(30), CustomerId = 2},
            new Invoice {Id = 4, Identifier = "INV-462579", ValidStartDate = new DateOnly(2024, 10, 19).AddDays(5), DueDate = new DateOnly(2024, 10, 19).AddDays(30), CustomerId = 3},
            new Invoice {Id = 5, Identifier = "INV-231645", ValidStartDate = new DateOnly(2024, 10, 19).AddDays(5), DueDate = new DateOnly(2024, 10, 19).AddDays(30), CustomerId = 3},
            new Invoice {Id = 6, Identifier = "INV-465281", ValidStartDate = new DateOnly(2024, 10, 14).AddDays(5), DueDate = new DateOnly(2024, 10, 14).AddDays(30), CustomerId = 4},
            new Invoice {Id = 7, Identifier = "INV-356891", ValidStartDate = new DateOnly(2024, 10, 14).AddDays(5), DueDate = new DateOnly(2024, 10, 14).AddDays(30), CustomerId = 4},
            new Invoice {Id = 8, Identifier = "INV-197846", ValidStartDate = new DateOnly(2024, 10, 17).AddDays(5), DueDate = new DateOnly(2024, 10, 17).AddDays(30), CustomerId = 4},
            new Invoice {Id = 9, Identifier = "INV-1387541", ValidStartDate = new DateOnly(2024, 10, 20).AddDays(5), DueDate = new DateOnly(2024, 10, 20).AddDays(30), CustomerId = 5},
            new Invoice {Id = 10, Identifier = "INV-978548", ValidStartDate = new DateOnly(2024, 10, 20).AddDays(5), DueDate = new DateOnly(2024, 10, 20).AddDays(30), CustomerId = 5},
            new Invoice {Id = 11, Identifier = "INV-159632", ValidStartDate = new DateOnly(2024, 10, 11).AddDays(5), DueDate = new DateOnly(2024, 10, 11).AddDays(30), CustomerId = 5},
            new Invoice {Id = 12, Identifier = "INV-1547862", ValidStartDate = new DateOnly(2024, 10, 8).AddDays(5), DueDate = new DateOnly(2024, 10, 8).AddDays(30), CustomerId = 2}
            
        );

        builder.Entity<InvoiceDetails>().HasData(
            new InvoiceDetails {Id = 1, SubTotal = 14999, Tax = 18, Total = 16000 , BillingAddressId = 1, InvoiceId = 1},
            new InvoiceDetails {Id = 2, SubTotal = 10000, Tax = 18, Total = 11111 , BillingAddressId = 1, InvoiceId = 2},
            new InvoiceDetails {Id = 3, SubTotal = 45000, Tax = 18, Total = 50000 , BillingAddressId = 3, InvoiceId = 3},
            new InvoiceDetails {Id = 4, SubTotal = 3900, Tax = 18, Total = 4999 , BillingAddressId = 4, InvoiceId = 4},
            new InvoiceDetails {Id = 5, SubTotal = 8000, Tax = 18, Total = 9999 , BillingAddressId = 4, InvoiceId = 5},
            new InvoiceDetails {Id = 6, SubTotal = 4560, Tax = 18, Total = 5000 , BillingAddressId = 6, InvoiceId = 6},
            new InvoiceDetails {Id = 7, SubTotal = 12000, Tax = 18, Total = 12890 , BillingAddressId = 6, InvoiceId = 7},
            new InvoiceDetails {Id = 8, SubTotal = 6500, Tax = 18, Total = 8560 , BillingAddressId = 6, InvoiceId = 8},
            new InvoiceDetails {Id = 9, SubTotal = 13465, Tax = 18, Total = 143656 , BillingAddressId = 8, InvoiceId = 9},
            new InvoiceDetails {Id = 10, SubTotal = 9000, Tax = 18, Total = 11000 , BillingAddressId = 8, InvoiceId = 10},
            new InvoiceDetails {Id = 11, SubTotal = 1354, Tax = 18, Total = 2700 , BillingAddressId = 8, InvoiceId = 11},
            new InvoiceDetails {Id = 12, SubTotal = 2345, Tax = 18, Total = 2356 , BillingAddressId = 3, InvoiceId = 12}
        );

        builder.Entity<InvoiceLineItem>().HasData(
            //Invoice 1
            new InvoiceLineItem {Id = 1, Quantity = 4, ItemId = 1, InvoiceDetailsId = 1},
            new InvoiceLineItem {Id = 2, Quantity = 3, ItemId = 2, InvoiceDetailsId = 1},
            new InvoiceLineItem {Id = 3, Quantity = 4, ItemId = 1, InvoiceDetailsId = 1},
            //Invoice 2
            new InvoiceLineItem {Id = 4, Quantity = 2, ItemId = 3, InvoiceDetailsId = 2},
            new InvoiceLineItem {Id = 5, Quantity = 1, ItemId = 6, InvoiceDetailsId = 2},
            //Invoice 3
            new InvoiceLineItem {Id = 6, Quantity = 1, ItemId = 4, InvoiceDetailsId = 3},
            new InvoiceLineItem {Id = 7, Quantity = 1, ItemId = 4, InvoiceDetailsId = 4},
            new InvoiceLineItem {Id = 8, Quantity = 1, ItemId = 4, InvoiceDetailsId = 4},
            //Invoice 4
            new InvoiceLineItem {Id = 9, Quantity = 1, ItemId = 5, InvoiceDetailsId = 4},
            new InvoiceLineItem {Id = 10, Quantity = 2, ItemId = 6, InvoiceDetailsId = 4},
            //Invoice 5
            new InvoiceLineItem {Id = 12, Quantity = 1, ItemId = 2, InvoiceDetailsId = 5},
            new InvoiceLineItem {Id = 13, Quantity = 1, ItemId = 3, InvoiceDetailsId = 5},
            //Invoice 6
            new InvoiceLineItem {Id = 14, Quantity = 1, ItemId = 3, InvoiceDetailsId = 6},
            //Invoice 7
            new InvoiceLineItem {Id = 15, Quantity = 1, ItemId = 1, InvoiceDetailsId = 7},
            new InvoiceLineItem {Id = 16, Quantity = 1, ItemId = 2, InvoiceDetailsId = 7},
            //Invoice 8
            new InvoiceLineItem {Id = 17, Quantity = 1, ItemId = 2, InvoiceDetailsId = 8},
            new InvoiceLineItem {Id = 18, Quantity = 3, ItemId = 6, InvoiceDetailsId = 8},
            //Invoice 9
            new InvoiceLineItem {Id = 19, Quantity = 1, ItemId = 4, InvoiceDetailsId = 1},
            //Invoice 10
            new InvoiceLineItem {Id = 20, Quantity = 1, ItemId = 1, InvoiceDetailsId = 1},
            //Invioce 11
            new InvoiceLineItem {Id = 21, Quantity = 1, ItemId = 6, InvoiceDetailsId = 11},
            //Invoice 12
            new InvoiceLineItem {Id = 22, Quantity = 1, ItemId = 5, InvoiceDetailsId = 12},
            new InvoiceLineItem {Id = 23, Quantity = 1, ItemId = 6, InvoiceDetailsId = 12}
        );
    }
}
