using System;
using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Contracts;

public interface IItemService
{
    public IEnumerable<Item> GetAllItems();
    public Item GetItemById(int id);
    public Item CreateItem(Item item);
    public Item UpdateItem(Item item);
    public void DeleteItem(int id);
}
