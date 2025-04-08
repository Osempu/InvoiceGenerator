using InvoiceGenerator.Application.Configurations;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;
        public CustomerRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Customer?> CreateCustomerAsync(Customer customer)
        {
            var newCustomer = context.Add(customer).Entity;
            await context.SaveChangesAsync();
            return newCustomer;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new NullReferenceException($"Customer with id: {id} does not exist in database");
        
            context.Remove(customer);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChanges = false)
        {
            var customers = trackChanges ? await context.Customers.Include(x => x.Addresses).ToListAsync() 
                                         : await context.Customers.Include(x => x.Addresses).AsNoTracking().ToListAsync();
            return customers;
        }

        public async Task<Customer?> GetCustomerAsync(int id, bool trackChanges = false)
        {
            var customer = trackChanges ? await context.Customers
                                                .Include(x => x.Addresses)
                                                .FirstOrDefaultAsync(x => x.Id == id)
                                          : await context.Customers
                                                .AsNoTracking()
                                                .Include(x => x.Addresses)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            return customer;
        }

        public async Task<Customer?> UpdateCustomerAsync(Customer customer)
        {
            context.Entry(customer).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return customer;
        }
    }
}