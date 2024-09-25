using Entities;

namespace Persistence.Repository
{
    public interface IClientRepository
    {
        public Task<Client> CreateAsync(Client client);
        public Task<Client> RemoveAsync(int id);
        public Task<Client> UpdateAsync(Client client);

        public IQueryable<Client> SoftFilterPageAsync(int position, int pageSize, string? name);
        public Task<Client?> GetByTINAsync(string TIN);
    }
}
