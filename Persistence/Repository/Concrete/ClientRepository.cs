using Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Persistence.Repository.Concrete
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client> CreateAsync(Client client)
        {
            _context.clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }
        public async Task<Client> RemoveAsync(int id)
        {
            Client client = _context.clients
                .Include(f => f.Founders)
                .Single(c => c.Id == id);
            _context.Remove(client);
            await _context.SaveChangesAsync();

            return client;
        }
        public async Task<Client> UpdateAsync(Client client)
        {
            _context.clients.Update(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public IQueryable<Client> SoftFilterPageAsync(int position, int pageSize, string? name)
        {
            var clientQuery = _context.clients.AsNoTracking();

            if (!string.IsNullOrEmpty(name))
            {
                clientQuery = clientQuery.Where(c => c.Name.Contains(name));
            }

            clientQuery = clientQuery
                .OrderBy(c => c.UpdateDate)
                .ThenBy(c => c.CreationDate)
                .Skip(position * pageSize)
                .Take(pageSize);

            return clientQuery;
        }

        public async Task<Client?> GetByTINAsync(string TIN)
        {
            Client? client = await _context.clients
                .FirstOrDefaultAsync(c => c.TIN == TIN);
            return client;
        }
    }
}
