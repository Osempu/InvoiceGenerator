using InvoiceGenerator.Application.Configurations;
using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Repositories
{
    public class ClientRepository : ICustomerRepository
    {
        private readonly AppDbContext context;
        public ClientRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            var newCustomer = context.Add(customer).Entity;
            await context.SaveChangesAsync();
            return newCustomer;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var Customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            context.Remove(Customer);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customers = await context.Customers.AsNoTracking()
                                                .ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            var customer = await context.Customers.AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.Id == id);
            return customer;
        }

        public async Task<Customer> UpdateCustomerASync(Customer customer)
        {
            context.Entry(customer).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return customer;
        }
    }
}