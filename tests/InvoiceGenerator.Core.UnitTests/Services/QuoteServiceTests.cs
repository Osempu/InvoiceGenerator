using System;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Core.Services;

namespace InvoiceGenerator.Core.UnitTests.Services;


public class QuoteServiceTests
{
    private readonly QuoteService _sut;
    public QuoteServiceTests()
    {
        _sut = new QuoteService();
    }

    [Fact]
    public void GetQuoteTotal_ShouldReturnCorrectTotal()
    {
        //Arrange
        var invoiceDetails = new InvoiceDetails
        {
            SubTotal = 0,
            Tax = 15,
            InvoiceLineItems = new List<InvoiceLineItem>
            {
                new InvoiceLineItem
                {
                    Quantity = 2,
                    Item = new Item { Price = 50m , Name = "Service A"}
                },
                new InvoiceLineItem
                {
                    Quantity = 1,
                    Item = new Item { Price = 100m, Name = "Service B"}
                }
            },
        };
        decimal subtotal = (2 * 50m) + (1 * 100m); // 200m
        decimal expectedTotal = subtotal + invoiceDetails.Tax; // 200 + 15
        //Act
        var servicesCost = _sut.CalculateServicesCost(invoiceDetails);

        //Assert
        Assert.Equal(expectedTotal, servicesCost.Total);
    }
}

