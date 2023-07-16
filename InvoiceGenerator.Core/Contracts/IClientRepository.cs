using InvoiceGenerator.Core.Models;

namespace InvoiceGenerator.Core.Contracts
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientAsync(int id);
        Task<Client> CreateClientAsync(Client client);
        Task<Client> UpdateClientASync(Client client);
        Task DeleteClientAsync(int id);
    }
}