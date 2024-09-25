using Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository.Concrete
{
    public class FounderRepository : IFounderRepository
    {
        private readonly AppDbContext _context;
        public FounderRepository(AppDbContext context) 
        { 
            _context = context;
        }
        public async Task<Founder> CreateAsync(Founder founder)
        {
            _context.founders.Add(founder);
            await _context.SaveChangesAsync();
            return founder;
        }

        public async Task<Founder> RemoveAsync(int id)
        {
            Founder founder = _context.founders
                .Single(f => f.Id == id);
            _context.Remove(founder);
            await _context.SaveChangesAsync();
            return founder;
        }

        public async Task<Founder> UpdateAsync(Founder founder)
        {
            Client client = _context.clients.Single(c => c.Id == founder.Id);
            _context.founders.Update(founder);
            await _context.SaveChangesAsync();
            return founder;
        }

        public async Task<Founder?> GetByTINAsync(string TIN)
        {
            Founder? founder = await _context.founders
                .FirstOrDefaultAsync(f => f.TIN == TIN);
            return founder;
        }
    }
}
