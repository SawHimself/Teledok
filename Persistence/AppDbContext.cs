using Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Client> clients { get; set; }
        public DbSet<Founder> founders { get; set; }
    }
}
