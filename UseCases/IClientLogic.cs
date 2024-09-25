using Entities;

namespace UseCases
{
    public interface IClientLogic
    {
        public IQueryable<Client> GetClients(int position, int pageSize, string? name);
        public Task<Client?> Create(Client newclient);
        public Task<Client?> Remove(string tin);
        public Task<Client?> UpdateTIN(string newTIN, string oldTIN, ClientType clietnType);
        public Task<Client?> UpdateName(string tin, string name);
        public Task<Client?> AddFounder(string tin, Founder founder);
        public Task<Client?> DeleteFounder(string clientTIN, string founderTIN);
    }
}