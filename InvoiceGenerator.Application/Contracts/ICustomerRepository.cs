using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Application.Configurations
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChanges = false);
        Task<Customer?> GetCustomerAsync(int id, bool trackChanges = false);
        Task<Customer?> CreateCustomerAsync(Customer client);
        Task<Customer?> UpdateCustomerAsync(Customer client);
        Task DeleteCustomerAsync(int id);
    }
}