using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Application.Configurations
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer client);
        Task<Customer> UpdateCustomerASync(Customer client);
        Task DeleteCustomerAsync(int id);
    }
}