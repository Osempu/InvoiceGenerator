using InvoiceGenerator.Application.Contracts;
using InvoiceGenerator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext context;
    public ItemRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<Item?> CreateItemAsync(Item item)
    {
        var newItem = context.Add(item).Entity;
        await context.SaveChangesAsync();
        return newItem;
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var item = await context.Items.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NullReferenceException($"Item with id {id} does not exist in database");

        context.Remove(item);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Item>> GetAllItemsAsync()
    {
        var items = await context.Items.AsNoTracking().ToListAsync();

        return items;
    }

    public async Task<Item?> GetItemAsync(int id)
    {
        return await context.Items.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Item?> UpdateItemASync(Item item)
    {
        context.Entry(item).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return item;
    }
}
