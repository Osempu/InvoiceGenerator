using System;

namespace InvoiceGenerator.Core.Models;

public static class AddressType
{
    public const string Billing = "Billing";
    public const string Shipping = "Shipping";

    public static readonly List<string> validAddressTypes = [Billing, Shipping];
}
