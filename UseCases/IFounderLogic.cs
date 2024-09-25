using Entities;
using Persistence.Repository;

namespace UseCases
{
    public interface IFounderLogic
    {
        public Task<Founder?> UpdateTIN(string TIN, string tin);
        public Task<Founder?> UpdateFullName(string TIN, string fullName);
    }
}