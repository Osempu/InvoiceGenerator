using InvoiceGenerator.Core.Contracts;
using InvoiceGenerator.Core.Models;
using InvoiceGenerator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext context;
        public ClientRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            var newClient = context.Add(client).Entity;
            await context.SaveChangesAsync();
            return newClient;
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            context.Remove(client);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            var clients = await context.Clients.AsNoTracking()
                                                .ToListAsync();
            return clients;
        }

        public async Task<Client> GetClientAsync(int id)
        {
            var client = await context.Clients.AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.Id == id);
            return client;
        }

        public async Task<Client> UpdateClientASync(Client client)
        {
            context.Entry(client).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return client;
        }
    }
}