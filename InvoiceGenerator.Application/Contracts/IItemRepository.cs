using System;
using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Application.Contracts;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllItemsAsync();
    Task<Item?> GetItemAsync(int id);
    Task<Item?> CreateItemAsync(Item item);
    Task<Item?> UpdateItemASync(Item item);
    Task DeleteCustomerAsync(int id);
}
