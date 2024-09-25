using Entities;

namespace Persistence.Repository
{
    public interface IFounderRepository
    {
        public Task<Founder> CreateAsync(Founder founder);
        public Task<Founder> RemoveAsync(int id);
        public Task<Founder> UpdateAsync(Founder founder);
        public Task<Founder?> GetByTINAsync(string TIN);
    }
}
